// Wms.Domain/Repositories/IUnitOfWork.cs

namespace Wms.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IItemRepository Items { get; }
    ILocationRepository Locations { get; }
    IStockRepository Stock { get; }
    IMovementRepository Movements { get; }
    ILotRepository Lots { get; }
    IUserRepository Users { get; }
    IAppTokenRepository AppTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}