using Microsoft.AspNetCore.Mvc;
using Wms.Application.UseCases.Locations;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

public class LocationsController : Controller
{
    private const string CurrentUserId = "WEB_USER";
    private readonly ICreateLocationUseCase _createLocationUseCase;
    private readonly IGetLocationsUseCase _getLocationsUseCase;
    private readonly IUpdateLocationUseCase _updateLocationUseCase;
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(
        IGetLocationsUseCase getLocationsUseCase,
        ICreateLocationUseCase createLocationUseCase,
        IUpdateLocationUseCase updateLocationUseCase,
        ILogger<LocationsController> logger)
    {
        _getLocationsUseCase = getLocationsUseCase;
        _createLocationUseCase = createLocationUseCase;
        _updateLocationUseCase = updateLocationUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? searchTerm)
    {
        try
        {
            var result = await _getLocationsUseCase.ExecuteAsync();

            var model = new LocationManagementViewModel
            {
                SearchTerm = searchTerm
            };

            if (result.IsSuccess)
            {
                var locations = result.Value.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    locations = locations.Where(l =>
                        l.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                }

                model.Locations = locations.ToList();
            }
            else
            {
                TempData["ErrorMessage"] = result.Error;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading locations");
            TempData["ErrorMessage"] = "Error al cargar las ubicaciones. Por favor, intente nuevamente.";
            return View(new LocationManagementViewModel());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateLocationViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLocationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new CreateLocationDto(
                model.Code,
                model.Name,
                model.WarehouseId,
                null, // ParentLocationId
                model.IsPickable,
                model.IsReceivable,
                model.Capacity
            );

            var result = await _createLocationUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Ubicación '{model.Name}' creada exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating location");
            TempData["ErrorMessage"] = "Error al crear la ubicación. Por favor, intente nuevamente.";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var result = await _getLocationsUseCase.ExecuteAsync();
            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }

            var location = result.Value.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                TempData["ErrorMessage"] = "Ubicación no encontrada";
                return RedirectToAction(nameof(Index));
            }

            var model = new EditLocationViewModel
            {
                Id = location.Id,
                Code = location.Code,
                Name = location.Name,
                IsPickable = location.IsPickable,
                IsReceivable = location.IsReceivable,
                Capacity = location.Capacity
            };

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading location for edit");
            TempData["ErrorMessage"] = "Error al cargar la ubicación. Por favor, intente nuevamente.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditLocationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new UpdateLocationDto(
                model.Id,
                model.Name,
                model.IsPickable,
                model.IsReceivable,
                model.Capacity
            );

            var result = await _updateLocationUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

            TempData["SuccessMessage"] = $"¡Ubicación '{model.Name}' actualizada exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating location");
            TempData["ErrorMessage"] = "Error al actualizar la ubicación. Por favor, intente nuevamente.";
            return View(model);
        }
    }
}