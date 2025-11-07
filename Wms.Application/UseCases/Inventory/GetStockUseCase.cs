// Wms.Application/UseCases/Inventory/GetStockUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;

namespace Wms.Application.UseCases.Inventory;

public interface IGetStockUseCase
{
    Task<Result<IEnumerable<StockDto>>> GetAllStockAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<StockDto>>> GetStockByItemAsync(int itemId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<StockDto>>> GetStockByLocationAsync(int locationId,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<StockSummaryDto>>> GetStockSummaryAsync(CancellationToken cancellationToken = default);
}

public class GetStockUseCase : IGetStockUseCase
{
    private readonly ILogger<GetStockUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetStockUseCase(IUnitOfWork unitOfWork, ILogger<GetStockUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<StockDto>>> GetAllStockAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var stockItems = await _unitOfWork.Stock.GetAllAsync(cancellationToken);
            var stockDtos = stockItems.Select(MapToDto);
            return Result.Success(stockDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all stock");
            return Result.Failure<IEnumerable<StockDto>>($"Error al obtener el stock: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<StockDto>>> GetStockByItemAsync(int itemId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stockItems = await _unitOfWork.Stock.GetByItemIdAsync(itemId, cancellationToken);
            var stockDtos = stockItems.Select(MapToDto);
            return Result.Success(stockDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stock for item {ItemId}", itemId);
            return Result.Failure<IEnumerable<StockDto>>($"Error al obtener el stock: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<StockDto>>> GetStockByLocationAsync(int locationId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stockItems = await _unitOfWork.Stock.GetByLocationIdAsync(locationId, cancellationToken);
            var stockDtos = stockItems.Select(MapToDto);
            return Result.Success(stockDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stock for location {LocationId}", locationId);
            return Result.Failure<IEnumerable<StockDto>>($"Error al obtener el stock: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<StockSummaryDto>>> GetStockSummaryAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stockItems = await _unitOfWork.Stock.GetAllAsync(cancellationToken);

            var summaries = stockItems
                .GroupBy(s => new { s.Item.Sku, s.Item.Name, s.Item.Price })
                .Select(g => new StockSummaryDto(
                    g.Key.Sku,
                    g.Key.Name,
                    g.Sum(s => s.QuantityAvailable.Value),
                    g.Sum(s => s.QuantityReserved.Value),
                    g.Sum(s => s.GetAvailableQuantity().Value),
                    g.Key.Price,
                    g.Key.Price.HasValue ? g.Sum(s => s.GetAvailableQuantity().Value) * g.Key.Price.Value : null,
                    g.Select(s => s.LocationId).Distinct().Count()
                ));

            return Result.Success(summaries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stock summary");
            return Result.Failure<IEnumerable<StockSummaryDto>>($"Error al obtener el resumen de stock: {ex.Message}");
        }
    }

    private static StockDto MapToDto(Stock stock)
    {
        var itemPrice = stock.Item.Price;
        decimal? totalValue = itemPrice.HasValue 
            ? stock.GetAvailableQuantity().Value * itemPrice.Value 
            : null;

        return new StockDto(
            stock.Id,
            stock.ItemId,
            stock.Item.Sku,
            stock.Item.Name,
            stock.LocationId,
            stock.Location.Code,
            stock.Location.Name,
            stock.LotId,
            stock.Lot?.Number,
            stock.SerialNumber,
            stock.QuantityAvailable.Value,
            stock.QuantityReserved.Value,
            stock.GetAvailableQuantity().Value,
            itemPrice,
            totalValue,
            stock.CreatedAt,
            stock.UpdatedAt
        );
    }
}