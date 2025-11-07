using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.ASP.Models;
using CreateItemDto = Wms.Application.UseCases.Items.CreateItemDto;

namespace Wms.ASP.Controllers;

[Authorize]
public class ItemsController : Controller
{
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.Identity?.Name ?? "UNKNOWN";
    private readonly ICreateItemUseCase _createItemUseCase;
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly ILogger<ItemsController> _logger;
    private readonly IUpdateItemUseCase _updateItemUseCase;

    public ItemsController(
        IGetItemsUseCase getItemsUseCase,
        ICreateItemUseCase createItemUseCase,
        IUpdateItemUseCase updateItemUseCase,
        ILogger<ItemsController> logger)
    {
        _getItemsUseCase = getItemsUseCase;
        _createItemUseCase = createItemUseCase;
        _updateItemUseCase = updateItemUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? searchTerm)
    {
        try
        {
            var result = await _getItemsUseCase.ExecuteAsync(searchTerm);

            var model = new ItemManagementViewModel
            {
                SearchTerm = searchTerm
            };

            if (result.IsSuccess)
            {
                model.Items = result.Value.ToList();
            }
            else
            {
                TempData["ErrorMessage"] = result.Error;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading items");
            TempData["ErrorMessage"] = "Error al cargar los artículos. Por favor, intente nuevamente.";
            return View(new ItemManagementViewModel());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateItemViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var barcodes = !string.IsNullOrWhiteSpace(model.Barcode)
                ? new List<string> { model.Barcode }
                : new List<string>();

            var request = new CreateItemDto(
                model.Sku,
                model.Name,
                model.Description ?? string.Empty,
                model.UnitOfMeasure ?? "EA",
                model.RequiresLot,
                model.RequiresSerial,
                0, // ShelfLifeDays
                model.Price,
                barcodes
            );

            var result = await _createItemUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Artículo '{model.Name}' creado exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating item");
            TempData["ErrorMessage"] = "Error al crear el artículo. Por favor, intente nuevamente.";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var result = await _getItemsUseCase.ExecuteAsync();

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }

            var item = result.Value.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                TempData["ErrorMessage"] = "Artículo no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var model = new EditItemViewModel
            {
                Id = item.Id,
                Sku = item.Sku,
                Name = item.Name,
                Description = item.Description,
                UnitOfMeasure = item.UnitOfMeasure,
                Price = item.Price,
                RequiresLot = item.RequiresLot,
                RequiresSerial = item.RequiresSerial,
                Barcode = item.Barcodes.FirstOrDefault()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading item for edit");
            TempData["ErrorMessage"] = "Error al cargar el artículo. Por favor, intente nuevamente.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var barcodes = !string.IsNullOrWhiteSpace(model.Barcode)
                ? new List<string> { model.Barcode }
                : new List<string>();

            var request = new UpdateItemDto(
                model.Id,
                model.Name,
                model.Description ?? string.Empty,
                0, // ShelfLifeDays - not implemented in the view model yet
                model.Price,
                model.RequiresLot,
                model.RequiresSerial,
                barcodes
            );

            var result = await _updateItemUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Artículo '{model.Name}' actualizado exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item");
            TempData["ErrorMessage"] = "Error al actualizar el artículo. Por favor, intente nuevamente.";
            return View(model);
        }
    }
}