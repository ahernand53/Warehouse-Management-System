// Wms.Domain/Repositories/ILotRepository.cs

using Wms.Domain.Entities;

namespace Wms.Domain.Repositories;

public interface ILotRepository : IRepository<Lot>
{
    Task<Lot?> GetByNumberAndItemAsync(string lotNumber, int itemId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lot>> GetByItemIdAsync(int itemId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string lotNumber, int itemId, CancellationToken cancellationToken = default);
}

