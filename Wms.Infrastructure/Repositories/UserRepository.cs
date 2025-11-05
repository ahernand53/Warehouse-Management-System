// Wms.Infrastructure/Repositories/UserRepository.cs

using Microsoft.EntityFrameworkCore;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Infrastructure.Data;

namespace Wms.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(WmsDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Username == username.ToLowerInvariant(), cancellationToken);
    }

    public async Task<User?> GetByUsernameAndApplicationAsync(string username, string applicationName, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(
                u => u.Username == username.ToLowerInvariant() && 
                     u.ApplicationName == applicationName,
                cancellationToken);
    }

    public async Task<bool> UsernameExistsAsync(string username, string applicationName, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(
                u => u.Username == username.ToLowerInvariant() && 
                     u.ApplicationName == applicationName,
                cancellationToken);
    }
}

