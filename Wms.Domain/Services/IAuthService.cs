// Wms.Domain/Services/IAuthService.cs

using Wms.Domain.Entities;

namespace Wms.Domain.Services;

public interface IAuthService
{
    Task<(bool IsValid, User? User)> ValidateCredentialsAsync(string username, string password, string applicationName, CancellationToken cancellationToken = default);
    Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken = default);
    Task<bool> VerifyPasswordAsync(string password, string passwordHash, CancellationToken cancellationToken = default);
    Task<string> GenerateTokenAsync(int userId, CancellationToken cancellationToken = default);
    Task<AppToken?> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
}

