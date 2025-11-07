// Wms.Application/UseCases/Items/GetItemsUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Domain.ValueObjects;

namespace Wms.Application.UseCases.Items;

public interface IGetItemsUseCase
{
    Task<Result<IEnumerable<ItemDto>>> ExecuteAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);

    Task<Result<ItemDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ItemDto>> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<Result<ItemDto>> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
}

public class GetItemsUseCase : IGetItemsUseCase
{
    private readonly ILogger<GetItemsUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetItemsUseCase(IUnitOfWork unitOfWork, ILogger<GetItemsUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ItemDto>>> ExecuteAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var items = string.IsNullOrWhiteSpace(searchTerm)
                ? await _unitOfWork.Items.GetAllAsync(cancellationToken)
                : await _unitOfWork.Items.SearchAsync(searchTerm, cancellationToken);

            var itemDtos = items.Select(MapToDto);
            return Result.Success(itemDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving items");
            return Result.Failure<IEnumerable<ItemDto>>($"Error al obtener los artículos: {ex.Message}");
        }
    }

    public async Task<Result<ItemDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id, cancellationToken);
            if (item == null)
                return Result.Failure<ItemDto>($"No se encontró el artículo con ID {id}");

            return Result.Success(MapToDto(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item {ItemId}", id);
            return Result.Failure<ItemDto>($"Error al obtener el artículo: {ex.Message}");
        }
    }

    public async Task<Result<ItemDto>> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _unitOfWork.Items.GetBySkuAsync(sku, cancellationToken);
            if (item == null)
                return Result.Failure<ItemDto>($"No se encontró el artículo con SKU '{sku}'");

            return Result.Success(MapToDto(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item {ItemSku}", sku);
            return Result.Failure<ItemDto>($"Error al obtener el artículo: {ex.Message}");
        }
    }

    public async Task<Result<ItemDto>> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _unitOfWork.Items.GetByBarcodeAsync(new Barcode(barcode), cancellationToken);
            if (item == null)
                return Result.Failure<ItemDto>($"No se encontró el artículo con código de barras '{barcode}'");

            return Result.Success(MapToDto(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item by barcode {Barcode}", barcode);
            return Result.Failure<ItemDto>($"Error al obtener el artículo: {ex.Message}");
        }
    }

    private static ItemDto MapToDto(Item item)
    {
        return new ItemDto(
            item.Id,
            item.Sku,
            item.Name,
            item.Description,
            item.UnitOfMeasure,
            item.IsActive,
            item.RequiresLot,
            item.RequiresSerial,
            item.ShelfLifeDays,
            item.Price,
            item.Barcodes.Select(b => b.Value).ToList(),
            item.CreatedAt,
            item.UpdatedAt
        );
    }
}