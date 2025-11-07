using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wms.Application.UseCases.Picking;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

[Authorize]
public class PickingController : Controller
{
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.Identity?.Name ?? "UNKNOWN";
    private readonly ILogger<PickingController> _logger;
    private readonly IPickOrderUseCase _pickOrderUseCase;

    public PickingController(
        IPickOrderUseCase pickOrderUseCase,
        ILogger<PickingController> logger)
    {
        _pickOrderUseCase = pickOrderUseCase;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new PickingViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Pick(PickingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            var request = new PickItemDto(
                model.ItemSku,
                model.LocationCode,
                model.Quantity,
                model.OrderNumber,
                model.LotNumber,
                model.SerialNumber,
                model.Notes
            );

            var result = await _pickOrderUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View("Index", model);
            }

            TempData["SuccessMessage"] = $"¡Despacho completado exitosamente! ID de Movimiento: {result.Value.MovementId}";
            return View("Index", new PickingViewModel()); // Clear form for next entry
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing pick");
            TempData["ErrorMessage"] = "Error al realizar el despacho. Por favor, intente nuevamente.";
            return View("Index", model);
        }
    }
}