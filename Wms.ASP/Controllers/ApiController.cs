// Wms.ASP/Controllers/ApiController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.Application.UseCases.Locations;
using Wms.Application.UseCases.Lots;

namespace Wms.ASP.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly IGetLocationsUseCase _getLocationsUseCase;
    private readonly IGetLotsUseCase _getLotsUseCase;
    private readonly ILogger<ApiController> _logger;

    public ApiController(
        IGetItemsUseCase getItemsUseCase,
        IGetLocationsUseCase getLocationsUseCase,
        IGetLotsUseCase getLotsUseCase,
        ILogger<ApiController> logger)
    {
        _getItemsUseCase = getItemsUseCase;
        _getLocationsUseCase = getLocationsUseCase;
        _getLotsUseCase = getLotsUseCase;
        _logger = logger;
    }

    [HttpGet("items/search")]
    public async Task<IActionResult> SearchItems([FromQuery] string? term = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
            {
                return Ok(Array.Empty<ItemDto>());
            }

            var result = await _getItemsUseCase.ExecuteAsync(term, cancellationToken);
            
            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error });
            }

            var items = result.Value.Take(20).Select(item => new
            {
                id = item.Id,
                sku = item.Sku,
                name = item.Name,
                displayText = $"{item.Sku} - {item.Name}",
                value = item.Sku,
                requiresLot = item.RequiresLot
            });

            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching items");
            return StatusCode(500, new { error = "Error al buscar artículos" });
        }
    }

    [HttpGet("locations/search")]
    public async Task<IActionResult> SearchLocations([FromQuery] string? term = null, [FromQuery] string? type = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
            {
                return Ok(Array.Empty<object>());
            }

            Result<IEnumerable<LocationDto>> result;
            
            if (type == "receivable")
            {
                result = await _getLocationsUseCase.GetReceivableLocationsAsync(cancellationToken);
            }
            else if (type == "pickable")
            {
                result = await _getLocationsUseCase.GetPickableLocationsAsync(cancellationToken);
            }
            else
            {
                result = await _getLocationsUseCase.ExecuteAsync(term, cancellationToken);
            }

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error });
            }

            var locations = result.Value
                .Where(l => l.Code.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                           l.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .Select(location => new
                {
                    id = location.Id,
                    code = location.Code,
                    name = location.Name,
                    displayText = $"{location.Code} - {location.Name}",
                    value = location.Code
                });

            return Ok(locations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching locations");
            return StatusCode(500, new { error = "Error al buscar ubicaciones" });
        }
    }

    [HttpGet("lots/search")]
    public async Task<IActionResult> SearchLots([FromQuery] string? term = null, [FromQuery] int? itemId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!itemId.HasValue)
            {
                return Ok(Array.Empty<object>());
            }

            // If term is empty or null, get all lots for the item
            if (string.IsNullOrWhiteSpace(term))
            {
                var allLotsResult = await _getLotsUseCase.GetByItemIdAsync(itemId.Value, cancellationToken);
                
                if (allLotsResult.IsFailure)
                {
                    return BadRequest(new { error = allLotsResult.Error });
                }

                var allLots = allLotsResult.Value.Select(lot => new
                {
                    id = lot.Id,
                    number = lot.Number,
                    expiryDate = lot.ExpiryDate?.ToString("dd/MM/yyyy"),
                    displayText = lot.ExpiryDate.HasValue
                        ? $"{lot.Number} (Vence: {lot.ExpiryDate.Value:dd/MM/yyyy})"
                        : lot.Number,
                    value = lot.Number
                });

                return Ok(allLots);
            }

            // If term is provided and has at least 2 characters, search
            if (term.Length < 2)
            {
                return Ok(Array.Empty<object>());
            }

            var result = await _getLotsUseCase.SearchAsync(term, itemId.Value, cancellationToken);
            
            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error });
            }

            var lots = result.Value.Take(50).Select(lot => new
            {
                id = lot.Id,
                number = lot.Number,
                expiryDate = lot.ExpiryDate?.ToString("dd/MM/yyyy"),
                displayText = lot.ExpiryDate.HasValue
                    ? $"{lot.Number} (Vence: {lot.ExpiryDate.Value:dd/MM/yyyy})"
                    : lot.Number,
                value = lot.Number
            });

            return Ok(lots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching lots");
            return StatusCode(500, new { error = "Error al buscar lotes" });
        }
    }

    [HttpGet("lots/by-item/{itemId}")]
    public async Task<IActionResult> GetLotsByItem(int itemId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _getLotsUseCase.GetByItemIdAsync(itemId, cancellationToken);
            
            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error });
            }

            var lots = result.Value.Select(lot => new
            {
                id = lot.Id,
                number = lot.Number,
                expiryDate = lot.ExpiryDate?.ToString("dd/MM/yyyy"),
                displayText = lot.ExpiryDate.HasValue
                    ? $"{lot.Number} (Vence: {lot.ExpiryDate.Value:dd/MM/yyyy})"
                    : lot.Number,
                value = lot.Number
            });

            return Ok(lots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving lots for item {ItemId}", itemId);
            return StatusCode(500, new { error = "Error al obtener lotes" });
        }
    }
}

