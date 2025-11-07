// Wms.Application/UseCases/Picking/PickOrderUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Domain.Repositories;
using Wms.Domain.Services;
using Wms.Domain.ValueObjects;

namespace Wms.Application.UseCases.Picking;

public record PickItemDto(
    string ItemSku,
    string FromLocationCode,
    decimal Quantity,
    string? OrderNumber = null,
    string? LotNumber = null,
    string? SerialNumber = null,
    string? Notes = null
);

public record PickResultDto(
    int MovementId,
    string ItemSku,
    string FromLocationCode,
    decimal Quantity,
    string? OrderNumber,
    DateTime Timestamp
);

public interface IPickOrderUseCase
{
    Task<Result<PickResultDto>> ExecuteAsync(PickItemDto request, string userId,
        CancellationToken cancellationToken = default);
}

public class PickOrderUseCase : IPickOrderUseCase
{
    private readonly ILogger<PickOrderUseCase> _logger;
    private readonly IStockMovementService _stockMovementService;
    private readonly IUnitOfWork _unitOfWork;

    public PickOrderUseCase(IUnitOfWork unitOfWork, IStockMovementService stockMovementService,
        ILogger<PickOrderUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _stockMovementService = stockMovementService;
        _logger = logger;
    }

    public async Task<Result<PickResultDto>> ExecuteAsync(PickItemDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate item exists
            var item = await _unitOfWork.Items.GetBySkuAsync(request.ItemSku, cancellationToken);
            if (item == null)
                return Result.Failure<PickResultDto>($"No se encontró el artículo con SKU '{request.ItemSku}'");

            if (!item.IsActive)
                return Result.Failure<PickResultDto>($"El artículo '{request.ItemSku}' está inactivo");

            // Validate location exists and is pickable
            var location = await _unitOfWork.Locations.GetByCodeAsync(request.FromLocationCode, cancellationToken);
            if (location == null)
                return Result.Failure<PickResultDto>($"No se encontró la ubicación '{request.FromLocationCode}'");

            if (!location.IsPickable)
                return Result.Failure<PickResultDto>($"La ubicación '{request.FromLocationCode}' no es despachable");

            if (!location.IsActive)
                return Result.Failure<PickResultDto>($"La ubicación '{request.FromLocationCode}' está inactiva");

            // Validate stock availability
            var stock = await _unitOfWork.Stock.GetByItemAndLocationAsync(
                item.Id, location.Id, null, request.SerialNumber, cancellationToken);

            if (stock == null)
                return Result.Failure<PickResultDto>(
                    $"No se encontró stock para el artículo '{request.ItemSku}' en la ubicación '{request.FromLocationCode}'");

            var requestedQuantity = new Quantity(request.Quantity);
            if (stock.GetAvailableQuantity() < requestedQuantity)
                return Result.Failure<PickResultDto>(
                    $"Stock insuficiente. Disponible: {stock.GetAvailableQuantity()}, Solicitado: {requestedQuantity}");

            // Create the pick movement
            var movement = await _stockMovementService.PickAsync(
                item.Id, location.Id, requestedQuantity, userId,
                stock.LotId, request.SerialNumber, request.OrderNumber, request.Notes, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Item {ItemSku} picked: {Quantity} from {LocationCode} by {UserId} for order {OrderNumber}",
                request.ItemSku, request.Quantity, request.FromLocationCode, userId, request.OrderNumber);

            return Result.Success(new PickResultDto(
                movement.Id,
                request.ItemSku,
                request.FromLocationCode,
                request.Quantity,
                request.OrderNumber,
                movement.Timestamp
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error picking item {ItemSku}", request.ItemSku);
            return Result.Failure<PickResultDto>($"Error al despachar el artículo: {ex.Message}");
        }
    }
}