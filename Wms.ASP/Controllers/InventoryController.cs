using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wms.Application.UseCases.Inventory;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

[Authorize]
public class InventoryController : Controller
{
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.Identity?.Name ?? "UNKNOWN";
    private readonly IGetStockUseCase _getStockUseCase;
    private readonly ILogger<InventoryController> _logger;
    private readonly IStockAdjustmentUseCase _stockAdjustmentUseCase;

    public InventoryController(
        IGetStockUseCase getStockUseCase,
        IStockAdjustmentUseCase stockAdjustmentUseCase,
        ILogger<InventoryController> logger)
    {
        _getStockUseCase = getStockUseCase;
        _stockAdjustmentUseCase = stockAdjustmentUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? searchTerm, bool showSummary = false)
    {
        try
        {
            var model = new InventoryViewModel
            {
                SearchTerm = searchTerm,
                ShowSummary = showSummary
            };

            if (showSummary)
            {
                var summaryResult = await _getStockUseCase.GetStockSummaryAsync();
                if (summaryResult.IsSuccess)
                {
                    var summaries = summaryResult.Value.OrderBy(s => s.ItemSku).ToList();
                    model.StockSummary = summaries;
                    model.TotalInventoryValue = summaries.Sum(s => s.TotalValue ?? 0);
                }
            }
            else
            {
                var result = await _getStockUseCase.GetAllStockAsync();
                if (result.IsSuccess)
                {
                    var stockItems = result.Value.Where(s => s.AvailableQuantity > 0);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        stockItems = stockItems.Where(s =>
                            s.ItemSku.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            s.ItemName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            s.LocationCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                    }

                    var stockList = stockItems.ToList();
                    model.StockItems = stockList;
                    model.TotalInventoryValue = stockList.Sum(s => s.TotalValue ?? 0);
                }
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading inventory data");
            TempData["ErrorMessage"] = "Error al cargar los datos de inventario. Por favor, intente nuevamente.";
            return View(new InventoryViewModel());
        }
    }

    [HttpGet]
    public IActionResult Adjust(string itemSku, string locationCode, decimal currentQuantity)
    {
        var model = new StockAdjustmentViewModel
        {
            ItemSku = itemSku,
            LocationCode = locationCode,
            CurrentQuantity = currentQuantity,
            NewQuantity = currentQuantity
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Adjust(StockAdjustmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new StockAdjustmentDto(
                model.ItemSku,
                model.LocationCode,
                model.NewQuantity,
                model.Reason
            );

            var result = await _stockAdjustmentUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Inventario ajustado exitosamente! ID de Movimiento: {result.Value.MovementId}";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adjusting stock");
            TempData["ErrorMessage"] = "Error al ajustar el inventario. Por favor, intente nuevamente.";
            return View(model);
        }
    }
}