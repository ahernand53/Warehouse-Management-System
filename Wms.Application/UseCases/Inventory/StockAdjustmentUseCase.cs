// Wms.Application/UseCases/Inventory/StockAdjustmentUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Repositories;
using Wms.Domain.Services;
using Wms.Domain.ValueObjects;

namespace Wms.Application.UseCases.Inventory;

public record StockAdjustmentDto(
    string ItemSku,
    string LocationCode,
    decimal NewQuantity,
    string Reason,
    string? LotNumber = null,
    string? SerialNumber = null
);

public interface IStockAdjustmentUseCase
{
    Task<Result<ReceiptResultDto>> ExecuteAsync(StockAdjustmentDto request, string userId,
        CancellationToken cancellationToken = default);
}

public class StockAdjustmentUseCase : IStockAdjustmentUseCase
{
    private readonly ILogger<StockAdjustmentUseCase> _logger;
    private readonly IStockMovementService _stockMovementService;
    private readonly IUnitOfWork _unitOfWork;

    public StockAdjustmentUseCase(IUnitOfWork unitOfWork, IStockMovementService stockMovementService,
        ILogger<StockAdjustmentUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _stockMovementService = stockMovementService;
        _logger = logger;
    }

    public async Task<Result<ReceiptResultDto>> ExecuteAsync(StockAdjustmentDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate item exists
            var item = await _unitOfWork.Items.GetBySkuAsync(request.ItemSku, cancellationToken);
            if (item == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró el artículo con SKU '{request.ItemSku}'");

            // Validate location exists
            var location = await _unitOfWork.Locations.GetByCodeAsync(request.LocationCode, cancellationToken);
            if (location == null)
                return Result.Failure<ReceiptResultDto>($"No se encontró la ubicación '{request.LocationCode}'");

            if (!location.IsActive)
                return Result.Failure<ReceiptResultDto>($"La ubicación '{request.LocationCode}' está inactiva");

            // Validate reason is provided
            if (string.IsNullOrWhiteSpace(request.Reason))
                return Result.Failure<ReceiptResultDto>("Se requiere una razón para el ajuste");

            // Create the adjustment
            var newQuantity = new Quantity(request.NewQuantity);
            var movement = await _stockMovementService.AdjustAsync(
                item.Id, location.Id, newQuantity, userId, request.Reason,
                null, request.SerialNumber, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Stock adjusted: Item {ItemSku} in {LocationCode} to {NewQuantity} by {UserId}. Reason: {Reason}",
                request.ItemSku, request.LocationCode, request.NewQuantity, userId, request.Reason);

            return Result.Success(new ReceiptResultDto(
                movement.Id,
                request.ItemSku,
                request.LocationCode,
                request.NewQuantity,
                request.LotNumber,
                movement.Timestamp
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adjusting stock for item {ItemSku}", request.ItemSku);
            return Result.Failure<ReceiptResultDto>($"Error al ajustar el stock: {ex.Message}");
        }
    }
}