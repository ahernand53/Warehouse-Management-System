// Wms.Application/UseCases/Locations/CreateLocationUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;

namespace Wms.Application.UseCases.Locations;

public record CreateLocationDto(
    string Code,
    string Name,
    int WarehouseId,
    int? ParentLocationId = null,
    bool IsPickable = true,
    bool IsReceivable = true,
    int Capacity = 0
);

public record UpdateLocationDto(
    int Id,
    string Name,
    bool IsPickable = true,
    bool IsReceivable = true,
    int Capacity = 0
);

public interface ICreateLocationUseCase
{
    Task<Result<LocationDto>> ExecuteAsync(CreateLocationDto request, string userId,
        CancellationToken cancellationToken = default);
}

public interface IUpdateLocationUseCase
{
    Task<Result<LocationDto>> ExecuteAsync(UpdateLocationDto request, string userId,
        CancellationToken cancellationToken = default);
}

public interface IDeleteLocationUseCase
{
    Task<Result> ExecuteAsync(int locationId, string userId,
        CancellationToken cancellationToken = default);
}

public class CreateLocationUseCase : ICreateLocationUseCase
{
    private readonly ILogger<CreateLocationUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLocationUseCase(IUnitOfWork unitOfWork, ILogger<CreateLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LocationDto>> ExecuteAsync(CreateLocationDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if code already exists
            var existingLocation = await _unitOfWork.Locations.GetByCodeAsync(request.Code, cancellationToken);
            if (existingLocation != null)
                return Result.Failure<LocationDto>($"Ya existe una ubicación con el código '{request.Code}'");

            // Validate parent location if specified
            if (request.ParentLocationId.HasValue)
            {
                var parentLocation =
                    await _unitOfWork.Locations.GetByIdAsync(request.ParentLocationId.Value, cancellationToken);
                if (parentLocation == null)
                    return Result.Failure<LocationDto>($"No se encontró la ubicación padre con ID {request.ParentLocationId}");
            }

            // Create new location
            var location = new Location(
                request.Code,
                request.Name,
                request.WarehouseId,
                request.ParentLocationId);

            location.SetPickable(request.IsPickable);
            location.SetReceivable(request.IsReceivable);

            if (request.Capacity > 0)
                location.SetCapacity(request.Capacity);

            await _unitOfWork.Locations.AddAsync(location, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Location created: {LocationCode} by {UserId}", request.Code, userId);

            return Result.Success(MapToDto(location));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating location {LocationCode}", request.Code);
            return Result.Failure<LocationDto>($"Error al crear la ubicación: {ex.Message}");
        }
    }

    private static LocationDto MapToDto(Location location)
    {
        return new LocationDto(
            location.Id,
            location.Code,
            location.Name,
            location.WarehouseId,
            location.ParentLocationId,
            location.IsPickable,
            location.IsReceivable,
            location.IsActive,
            location.Capacity,
            location.GetFullPath(),
            location.CreatedAt,
            location.UpdatedAt
        );
    }
}

public class UpdateLocationUseCase : IUpdateLocationUseCase
{
    private readonly ILogger<UpdateLocationUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationUseCase(IUnitOfWork unitOfWork, ILogger<UpdateLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LocationDto>> ExecuteAsync(UpdateLocationDto request, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var location = await _unitOfWork.Locations.GetByIdAsync(request.Id, cancellationToken);
            if (location == null)
                return Result.Failure<LocationDto>($"No se encontró la ubicación con ID {request.Id}");

            // Update location details
            location.UpdateDetails(request.Name);
            location.SetPickable(request.IsPickable);
            location.SetReceivable(request.IsReceivable);

            if (request.Capacity >= 0)
                location.SetCapacity(request.Capacity);

            await _unitOfWork.Locations.UpdateAsync(location, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Location updated: {LocationCode} by {UserId}", location.Code, userId);

            return Result.Success(MapToDto(location));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating location {LocationId}", request.Id);
            return Result.Failure<LocationDto>($"Error al actualizar la ubicación: {ex.Message}");
        }
    }

    private static LocationDto MapToDto(Location location)
    {
        return new LocationDto(
            location.Id,
            location.Code,
            location.Name,
            location.WarehouseId,
            location.ParentLocationId,
            location.IsPickable,
            location.IsReceivable,
            location.IsActive,
            location.Capacity,
            location.GetFullPath(),
            location.CreatedAt,
            location.UpdatedAt
        );
    }
}

public class DeleteLocationUseCase : IDeleteLocationUseCase
{
    private readonly ILogger<DeleteLocationUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLocationUseCase(IUnitOfWork unitOfWork, ILogger<DeleteLocationUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> ExecuteAsync(int locationId, string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var location = await _unitOfWork.Locations.GetByIdAsync(locationId, cancellationToken);
            if (location == null)
                return Result.Failure($"No se encontró la ubicación con ID {locationId}");

            // Check if location has stock before deleting
            var stockItems = await _unitOfWork.Stock.GetByLocationIdAsync(locationId, cancellationToken);
            if (stockItems.Any(s => s.QuantityAvailable.Value > 0))
            {
                return Result.Failure("No se puede eliminar una ubicación con stock existente. Por favor, mueva el stock primero.");
            }

            // Check if location has child locations
            var childLocations = await _unitOfWork.Locations.GetChildLocationsAsync(locationId, cancellationToken);
            if (childLocations.Any())
            {
                return Result.Failure(
                    "No se puede eliminar una ubicación con ubicaciones hijas. Por favor, elimine las ubicaciones hijas primero.");
            }

            // Soft delete by deactivating
            location.Deactivate();
            await _unitOfWork.Locations.UpdateAsync(location, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Location deactivated: {LocationCode} by {UserId}", location.Code, userId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting location {LocationId}", locationId);
            return Result.Failure($"Error al eliminar la ubicación: {ex.Message}");
        }
    }
}