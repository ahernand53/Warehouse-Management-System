// Wms.Domain/Repositories/IUserRepository.cs

using Wms.Domain.Entities;

namespace Wms.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAndApplicationAsync(string username, string applicationName, CancellationToken cancellationToken = default);
    Task<bool> UsernameExistsAsync(string username, string applicationName, CancellationToken cancellationToken = default);
}

