using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Receiving;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

[Authorize]
public class ReceivingController : Controller
{
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.Identity?.Name ?? "UNKNOWN";
    private readonly ILogger<ReceivingController> _logger;
    private readonly IPutawayUseCase _putawayUseCase;
    private readonly IReceiveItemUseCase _receiveItemUseCase;

    public ReceivingController(
        IReceiveItemUseCase receiveItemUseCase,
        IPutawayUseCase putawayUseCase,
        ILogger<ReceivingController> logger)
    {
        _receiveItemUseCase = receiveItemUseCase;
        _putawayUseCase = putawayUseCase;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Receive()
    {
        return View(new ReceivingViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Receive(ReceivingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new ReceiveItemDto(
                model.ItemSku,
                model.LocationCode,
                model.Quantity,
                model.LotNumber,
                model.SerialNumber,
                model.ReferenceNumber,
                model.Notes
            );

            var result = await _receiveItemUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Artículo recibido exitosamente! ID de Movimiento: {result.Value.MovementId}";
            return View(new ReceivingViewModel()); // Clear form for next entry
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error receiving item");
            TempData["ErrorMessage"] = "Error al recibir el artículo. Por favor, intente nuevamente.";
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Putaway()
    {
        return View(new PutawayViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Putaway(PutawayViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new PutawayDto(
                model.ItemSku,
                model.FromLocationCode,
                model.ToLocationCode,
                model.Quantity,
                model.LotNumber,
                model.SerialNumber,
                model.Notes
            );

            var result = await _putawayUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Almacenamiento completado exitosamente! ID de Movimiento: {result.Value.MovementId}";
            return View(new PutawayViewModel()); // Clear form for next entry
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing putaway");
            TempData["ErrorMessage"] = "Error al realizar el almacenamiento. Por favor, intente nuevamente.";
            return View(model);
        }
    }
}