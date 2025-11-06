// Wms.Infrastructure/Repositories/LotRepository.cs

using Microsoft.EntityFrameworkCore;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Infrastructure.Data;

namespace Wms.Infrastructure.Repositories;

public class LotRepository : Repository<Lot>, ILotRepository
{
    public LotRepository(WmsDbContext context) : base(context)
    {
    }

    public async Task<Lot?> GetByNumberAndItemAsync(string lotNumber, int itemId, CancellationToken cancellationToken = default)
    {
        var normalizedLotNumber = lotNumber.Trim().ToUpperInvariant();
        return await _dbSet
            .FirstOrDefaultAsync(l => l.ItemId == itemId && l.Number == normalizedLotNumber, cancellationToken);
    }

    public async Task<IEnumerable<Lot>> GetByItemIdAsync(int itemId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(l => l.ItemId == itemId && l.IsActive)
            .OrderBy(l => l.Number)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string lotNumber, int itemId, CancellationToken cancellationToken = default)
    {
        var normalizedLotNumber = lotNumber.Trim().ToUpperInvariant();
        return await _dbSet
            .AnyAsync(l => l.ItemId == itemId && l.Number == normalizedLotNumber, cancellationToken);
    }
}

