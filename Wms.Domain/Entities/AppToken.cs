// Wms.Domain/Entities/AppToken.cs

using Wms.Domain.Common;

namespace Wms.Domain.Entities;

public class AppToken : Entity
{
    // EF Constructor
    private AppToken()
    {
    }

    public AppToken(int userId, string token, DateTime expiresAt, string? ipAddress = null)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token is required", nameof(token));

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future", nameof(expiresAt));

        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        IsRevoked = false;
        CreatedIpAddress = ipAddress;
    }

    public int UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? CreatedIpAddress { get; private set; }
    public DateTime? LastUsedAt { get; private set; }
    public string? LastUsedIpAddress { get; private set; }

    // Navigation property
    public User User { get; private set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsValid => !IsRevoked && !IsExpired;

    public void Revoke()
    {
        if (IsRevoked)
            return;

        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void RecordUsage(string? ipAddress = null)
    {
        LastUsedAt = DateTime.UtcNow;
        LastUsedIpAddress = ipAddress;
        SetUpdatedAt();
    }
}

