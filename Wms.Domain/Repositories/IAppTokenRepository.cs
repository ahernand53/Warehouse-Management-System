// Wms.Domain/Repositories/IAppTokenRepository.cs

using Wms.Domain.Entities;

namespace Wms.Domain.Repositories;

public interface IAppTokenRepository : IRepository<AppToken>
{
    Task<AppToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppToken>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task RevokeTokensByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task RevokeExpiredTokensAsync(CancellationToken cancellationToken = default);
}

