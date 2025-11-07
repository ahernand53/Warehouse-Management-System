using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.Application.UseCases.Locations;
using Wms.Application.UseCases.Reports;
using Wms.Domain.Enums;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

[Authorize]
public class MovementsController : Controller
{
    private readonly IMovementReportUseCase _movementReportUseCase;
    private readonly IGetLocationsUseCase _getLocationsUseCase;
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly ILogger<MovementsController> _logger;

    public MovementsController(
        IMovementReportUseCase movementReportUseCase,
        IGetLocationsUseCase getLocationsUseCase,
        IGetItemsUseCase getItemsUseCase,
        ILogger<MovementsController> logger)
    {
        _movementReportUseCase = movementReportUseCase;
        _getLocationsUseCase = getLocationsUseCase;
        _getItemsUseCase = getItemsUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index(
        string? itemSku = null,
        string? locationCode = null,
        string? movementType = null,
        DateTime? fromDate = null,
        DateTime? toDate = null)
    {
        try
        {
            // Parse movement type if provided
            MovementType? typeFilter = null;
            if (!string.IsNullOrWhiteSpace(movementType) && Enum.TryParse<MovementType>(movementType, true, out var parsedType))
            {
                typeFilter = parsedType;
            }

            // Set default date range to last 30 days if not provided
            if (!fromDate.HasValue)
                fromDate = DateTime.Today.AddDays(-30);
            if (!toDate.HasValue)
                toDate = DateTime.Today.AddDays(1);

            var request = new MovementReportRequest(
                fromDate,
                toDate,
                itemSku,
                locationCode,
                typeFilter,
                null
            );

            var result = await _movementReportUseCase.ExecuteAsync(request);

            // Get locations and items for dropdowns
            var locationsResult = await _getLocationsUseCase.ExecuteAsync();
            var itemsResult = await _getItemsUseCase.ExecuteAsync();

            var model = new MovementsViewModel
            {
                Movements = result.IsSuccess ? result.Value.ToList() : new List<MovementReportDto>(),
                ItemSku = itemSku,
                LocationCode = locationCode,
                MovementType = movementType,
                FromDate = fromDate ?? DateTime.Today.AddDays(-30),
                ToDate = toDate ?? DateTime.Today.AddDays(1),
                Locations = locationsResult.IsSuccess ? locationsResult.Value.ToList() : new List<LocationDto>(),
                Items = itemsResult.IsSuccess ? itemsResult.Value.ToList() : new List<ItemDto>(),
                MovementTypes = Enum.GetValues<MovementType>().Select(t => new MovementTypeOption
                {
                    Value = t.ToString(),
                    Name = GetMovementTypeName(t)
                }).ToList()
            };

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error;
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading movements");
            TempData["ErrorMessage"] = "Error al cargar los movimientos. Por favor, intente nuevamente.";
            return View(new MovementsViewModel());
        }
    }

    private static string GetMovementTypeName(MovementType type)
    {
        return type switch
        {
            MovementType.Receipt => "Recepción",
            MovementType.Putaway => "Almacenamiento",
            MovementType.Pick => "Despacho",
            MovementType.Adjustment => "Ajuste",
            _ => type.ToString()
        };
    }
}

