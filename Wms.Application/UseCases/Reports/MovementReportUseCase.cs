// Wms.Application/UseCases/Reports/MovementReportUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Domain.Entities;
using Wms.Domain.Enums;
using Wms.Domain.Repositories;

namespace Wms.Application.UseCases.Reports;

public record MovementReportDto(
    int Id,
    string Type,
    string ItemSku,
    string ItemName,
    string? FromLocationCode,
    string? ToLocationCode,
    decimal Quantity,
    string? LotNumber,
    string? SerialNumber,
    string UserId,
    string? ReferenceNumber,
    string? Notes,
    DateTime Timestamp
);

public record MovementReportRequest(
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    string? ItemSku = null,
    string? LocationCode = null,
    MovementType? MovementType = null,
    string? UserId = null
);

public interface IMovementReportUseCase
{
    Task<Result<IEnumerable<MovementReportDto>>> ExecuteAsync(MovementReportRequest request,
        CancellationToken cancellationToken = default);
}

public class MovementReportUseCase : IMovementReportUseCase
{
    private readonly ILogger<MovementReportUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MovementReportUseCase(IUnitOfWork unitOfWork, ILogger<MovementReportUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<MovementReportDto>>> ExecuteAsync(MovementReportRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var movements = await GetFilteredMovementsAsync(request, cancellationToken);
            var reportData = movements.Select(MapToDto);

            _logger.LogInformation("Movement report generated with {Count} records", reportData.Count());
            return Result.Success(reportData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating movement report");
            return Result.Failure<IEnumerable<MovementReportDto>>($"Error generating report: {ex.Message}");
        }
    }

    private async Task<IEnumerable<Movement>> GetFilteredMovementsAsync(
        MovementReportRequest request, CancellationToken cancellationToken)
    {
        // Get all movements with includes, then apply filters
        var allMovements = await _unitOfWork.Movements.GetAllAsync(cancellationToken);
        
        // Apply date range filter (default to last 30 days if not specified)
        var fromDate = request.FromDate ?? DateTime.Today.AddDays(-30);
        var toDate = request.ToDate ?? DateTime.Today.AddDays(1);
        
        var filtered = allMovements
            .Where(m => m.Timestamp >= fromDate && m.Timestamp <= toDate);

        // Apply movement type filter if provided
        if (request.MovementType.HasValue)
        {
            filtered = filtered.Where(m => m.Type == request.MovementType.Value);
        }

        // Apply item filter if provided
        if (!string.IsNullOrWhiteSpace(request.ItemSku))
        {
            filtered = filtered.Where(m => m.Item != null && m.Item.Sku.Contains(request.ItemSku, StringComparison.OrdinalIgnoreCase));
        }

        // Apply location filter if provided
        if (!string.IsNullOrWhiteSpace(request.LocationCode))
        {
            filtered = filtered.Where(m => 
                (m.FromLocation != null && m.FromLocation.Code.Contains(request.LocationCode, StringComparison.OrdinalIgnoreCase)) ||
                (m.ToLocation != null && m.ToLocation.Code.Contains(request.LocationCode, StringComparison.OrdinalIgnoreCase)));
        }

        // Apply user filter if provided
        if (!string.IsNullOrWhiteSpace(request.UserId))
        {
            filtered = filtered.Where(m => m.UserId.Contains(request.UserId, StringComparison.OrdinalIgnoreCase));
        }

        return filtered.OrderByDescending(m => m.Timestamp);
    }

    private static MovementReportDto MapToDto(Movement movement)
    {
        // Handle null references gracefully
        var itemSku = movement.Item?.Sku ?? "N/A";
        var itemName = movement.Item?.Name ?? "Artículo desconocido";
        
        return new MovementReportDto(
            movement.Id,
            movement.Type.ToString(),
            itemSku,
            itemName,
            movement.FromLocation?.Code,
            movement.ToLocation?.Code,
            movement.Quantity.Value,
            movement.Lot?.Number,
            movement.SerialNumber,
            movement.UserId,
            movement.ReferenceNumber,
            movement.Notes,
            movement.Timestamp
        );
    }
}