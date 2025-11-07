// Wms.Application/UseCases/Receiving/PutawayUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Repositories;
using Wms.Domain.Services;
using Wms.Domain.ValueObjects;

namespace Wms.Application.UseCases.Receiving;

public interface IPutawayUseCase
{
    Task<Result<ReceiptResultDto>> ExecuteAsync(PutawayDto request, string userId,
        CancellationToken cancellationToken = default);
}

public class PutawayUseCase : IPutawayUseCase
{
    private readonly ILogger<PutawayUseCase> _logger;
    private readonly IStockMovementService _stockMovementService;
    private readonly IUnitOfWork _unitOfWork;

    public PutawayUseCase(IUnitOfWork unitOfWork, IStockMovementService stockMovementService,
        ILogger<PutawayUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _stockMovementService = stockMovementService;
        _logger = logger;
    }

    public async Task<Result<ReceiptResultDto>> ExecuteAsync(PutawayDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate item exists
            var item = await _unitOfWork.Items.GetBySkuAsync(request.ItemSku, cancellationToken);
            if (item == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró el artículo con SKU '{request.ItemSku}'");

            // Validate from location
            var fromLocation = await _unitOfWork.Locations.GetByCodeAsync(request.FromLocationCode, cancellationToken);
            if (fromLocation == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró la ubicación origen '{request.FromLocationCode}'");

            // Validate to location
            var toLocation = await _unitOfWork.Locations.GetByCodeAsync(request.ToLocationCode, cancellationToken);
            if (toLocation == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró la ubicación destino '{request.ToLocationCode}'");

            if (!toLocation.IsReceivable)
                return Result.Failure<ReceiptResultDto>($"La ubicación '{request.ToLocationCode}' no es recibible");

            if (!toLocation.IsActive)
                return Result.Failure<ReceiptResultDto>($"La ubicación '{request.ToLocationCode}' está inactiva");

            // Validate stock exists in from location
            var stock = await _unitOfWork.Stock.GetByItemAndLocationAsync(
                item.Id, fromLocation.Id, null, request.SerialNumber, cancellationToken);

            if (stock == null)
                return Result.Failure<ReceiptResultDto>(
                    $"No se encontró stock para el artículo '{request.ItemSku}' en la ubicación '{request.FromLocationCode}'");

            var requestedQuantity = new Quantity(request.Quantity);
            if (stock.GetAvailableQuantity() < requestedQuantity)
                return Result.Failure<ReceiptResultDto>(
                    $"Stock insuficiente. Disponible: {stock.GetAvailableQuantity()}, Solicitado: {requestedQuantity}");

            // Create the putaway movement
            var movement = await _stockMovementService.PutawayAsync(
                item.Id, fromLocation.Id, toLocation.Id, requestedQuantity, userId,
                stock.LotId, request.SerialNumber, notes: request.Notes, cancellationToken: cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Item {ItemSku} putaway: {Quantity} from {FromLocation} to {ToLocation} by {UserId}",
                request.ItemSku, request.Quantity, request.FromLocationCode, request.ToLocationCode, userId);

            return Result.Success(new ReceiptResultDto(
                movement.Id,
                request.ItemSku,
                request.ToLocationCode,
                request.Quantity,
                request.LotNumber,
                movement.Timestamp
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during putaway for item {ItemSku}", request.ItemSku);
            return Result.Failure<ReceiptResultDto>($"Error durante el almacenamiento: {ex.Message}");
        }
    }
}