// Wms.Infrastructure/Services/AuthService.cs

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Domain.Services;

namespace Wms.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(bool IsValid, User? User)> ValidateCredentialsAsync(string username, string password, string applicationName, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByUsernameAndApplicationAsync(username, applicationName, cancellationToken);

        if (user == null || !user.IsActive)
        {
            return (false, null);
        }

        var isValid = await VerifyPasswordAsync(password, user.PasswordHash, cancellationToken);
        return (isValid, isValid ? user : null);
    }

    public Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken = default)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Task.FromResult(Convert.ToBase64String(hashedBytes));
    }

    public async Task<bool> VerifyPasswordAsync(string password, string passwordHash, CancellationToken cancellationToken = default)
    {
        var computedHash = await HashPasswordAsync(password, cancellationToken);
        return computedHash == passwordHash;
    }

    public async Task<string> GenerateTokenAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var secretKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
        var issuer = _configuration["Jwt:Issuer"] ?? "WMS";
        var audience = _configuration["Jwt:Audience"] ?? "WMS";
        var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "1440"); // Default 24 hours

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("ApplicationName", user.ApplicationName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Save token to database
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);
        var appToken = new AppToken(userId, tokenString, expiresAt);
        await _unitOfWork.AppTokens.AddAsync(appToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tokenString;
    }

    public async Task<AppToken?> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var appToken = await _unitOfWork.AppTokens.GetByTokenAsync(token, cancellationToken);
        
        if (appToken == null || !appToken.IsValid)
        {
            return null;
        }

        try
        {
            var secretKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
            var issuer = _configuration["Jwt:Issuer"] ?? "WMS";
            var audience = _configuration["Jwt:Audience"] ?? "WMS";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Record token usage
            appToken.RecordUsage();
            await _unitOfWork.AppTokens.UpdateAsync(appToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return appToken;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Token validation failed for token: {Token}", token.Substring(0, Math.Min(20, token.Length)));
            return null;
        }
    }
}

