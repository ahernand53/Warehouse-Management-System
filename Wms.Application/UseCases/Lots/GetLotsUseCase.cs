// Wms.Application/UseCases/Lots/GetLotsUseCase.cs

using Microsoft.Extensions.Logging;
using Wms.Application.Common;
using Wms.Application.DTOs;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;

namespace Wms.Application.UseCases.Lots;

public interface IGetLotsUseCase
{
    Task<Result<IEnumerable<LotDto>>> SearchAsync(string searchTerm, int? itemId = null,
        CancellationToken cancellationToken = default);
}

public class GetLotsUseCase : IGetLotsUseCase
{
    private readonly ILogger<GetLotsUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetLotsUseCase(IUnitOfWork unitOfWork, ILogger<GetLotsUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<LotDto>>> SearchAsync(string searchTerm, int? itemId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Result.Success(Enumerable.Empty<LotDto>());
            }

            var lots = await _unitOfWork.Lots.SearchAsync(searchTerm, itemId, cancellationToken);
            var lotDtos = lots.Select(MapToDto);
            return Result.Success(lotDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching lots with term {SearchTerm}", searchTerm);
            return Result.Failure<IEnumerable<LotDto>>($"Error searching lots: {ex.Message}");
        }
    }

    private static LotDto MapToDto(Lot lot)
    {
        return new LotDto(
            lot.Id,
            lot.Number,
            lot.ItemId,
            lot.Item?.Sku ?? string.Empty,
            lot.Item?.Name,
            lot.ExpiryDate,
            lot.ManufacturedDate,
            lot.IsActive
        );
    }
}

