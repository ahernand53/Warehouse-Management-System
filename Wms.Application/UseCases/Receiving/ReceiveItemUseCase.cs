// Wms.Application/UseCases/Receiving/ReceiveItemUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Domain.Services;
using Wms.Domain.ValueObjects;

namespace Wms.Application.UseCases.Receiving;

public interface IReceiveItemUseCase
{
    Task<Result<ReceiptResultDto>> ExecuteAsync(ReceiveItemDto request, string userId,
        CancellationToken cancellationToken = default);
}

public class ReceiveItemUseCase : IReceiveItemUseCase
{
    private readonly ILogger<ReceiveItemUseCase> _logger;
    private readonly IStockMovementService _stockMovementService;
    private readonly IUnitOfWork _unitOfWork;

    public ReceiveItemUseCase(IUnitOfWork unitOfWork, IStockMovementService stockMovementService,
        ILogger<ReceiveItemUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _stockMovementService = stockMovementService;
        _logger = logger;
    }

    public async Task<Result<ReceiptResultDto>> ExecuteAsync(ReceiveItemDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate item exists
            var item = await _unitOfWork.Items.GetBySkuAsync(request.ItemSku, cancellationToken);
            if (item == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró el artículo con SKU '{request.ItemSku}'");

            if (!item.IsActive)
                return Result.Failure<ReceiptResultDto>($"El artículo '{request.ItemSku}' está inactivo");

            // Validate location exists and is receivable
            var location = await _unitOfWork.Locations.GetByCodeAsync(request.LocationCode, cancellationToken);
            if (location == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró la ubicación '{request.LocationCode}'");

            if (!location.IsReceivable)
                return Result.Failure<ReceiptResultDto>($"La ubicación '{request.LocationCode}' no es recibible");

            if (!location.IsActive)
                return Result.Failure<ReceiptResultDto>($"La ubicación '{request.LocationCode}' está inactiva");

            // Handle lot creation if required
            int? lotId = null;
            if (!string.IsNullOrWhiteSpace(request.LotNumber))
            {
                var existingLot = await GetOrCreateLotAsync(item.Id, request.LotNumber,
                    request.ExpiryDate, request.ManufacturedDate, cancellationToken);
                
                // Save the lot first to ensure it has an ID before creating the movement
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                lotId = existingLot.Id;
            }
            else if (item.RequiresLot)
            {
                return Result.Failure<ReceiptResultDto>($"El artículo '{request.ItemSku}' requiere un número de lote");
            }

            // Validate serial number requirement
            if (item.RequiresSerial && string.IsNullOrWhiteSpace(request.SerialNumber))
                return Result.Failure<ReceiptResultDto>($"El artículo '{request.ItemSku}' requiere un número de serie");

            // Create the receipt movement
            var quantity = new Quantity(request.Quantity);
            var movement = await _stockMovementService.ReceiveAsync(
                item.Id, location.Id, quantity, userId, lotId,
                request.SerialNumber, request.ReferenceNumber, request.Notes, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Item {ItemSku} received: {Quantity} to {LocationCode} by {UserId}",
                request.ItemSku, request.Quantity, request.LocationCode, userId);

            return Result.Success(new ReceiptResultDto(
                movement.Id,
                request.ItemSku,
                request.LocationCode,
                request.Quantity,
                request.LotNumber,
                movement.Timestamp
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error receiving item {ItemSku}", request.ItemSku);
            return Result.Failure<ReceiptResultDto>($"Error al recibir el artículo: {ex.Message}");
        }
    }

    private async Task<Lot> GetOrCreateLotAsync(int itemId, string lotNumber,
        DateTime? expiryDate, DateTime? manufacturedDate, CancellationToken cancellationToken)
    {
        // Check if lot already exists
        var existingLot = await _unitOfWork.Lots.GetByNumberAndItemAsync(lotNumber, itemId, cancellationToken);
        
        if (existingLot != null)
        {
            // Update dates if provided and different
            if (expiryDate.HasValue || manufacturedDate.HasValue)
            {
                existingLot.UpdateDates(expiryDate, manufacturedDate);
                await _unitOfWork.Lots.UpdateAsync(existingLot, cancellationToken);
            }
            return existingLot;
        }

        // Create new lot
        var lot = new Lot(lotNumber, itemId, expiryDate, manufacturedDate);
        return await _unitOfWork.Lots.AddAsync(lot, cancellationToken);
    }
}