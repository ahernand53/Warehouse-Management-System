// Wms.Domain/Entities/User.cs

using Wms.Domain.Common;

namespace Wms.Domain.Entities;

public class User : Entity
{
    // EF Constructor
    private User()
    {
    }

    public User(string username, string passwordHash, string applicationName, bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required", nameof(username));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(applicationName))
            throw new ArgumentException("Application name is required", nameof(applicationName));

        Username = username.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        ApplicationName = applicationName.Trim();
        IsActive = isActive;
    }

    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string ApplicationName { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string? LastLoginIp { get; private set; }

    public void UpdatePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        PasswordHash = passwordHash;
        SetUpdatedAt();
    }

    public void RecordLogin(string? ipAddress = null)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIp = ipAddress;
        SetUpdatedAt();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }
}

