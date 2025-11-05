using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wms.Domain.Services;
using Wms.ASP.Models;

namespace Wms.ASP.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private const string ApplicationName = "WMS";

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToLocal(returnUrl);
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var (isValid, user) = await _authService.ValidateCredentialsAsync(
                model.Username,
                model.Password,
                ApplicationName);

            if (!isValid || user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return View(model);
            }

            // Generate token
            var token = await _authService.GenerateTokenAsync(user.Id);

            // Record login - get fresh user from repository to update
            var unitOfWork = HttpContext.RequestServices.GetRequiredService<Wms.Domain.Repositories.IUnitOfWork>();
            var userToUpdate = await unitOfWork.Users.GetByIdAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.RecordLogin(HttpContext.Connection.RemoteIpAddress?.ToString());
                await unitOfWork.Users.UpdateAsync(userToUpdate);
                await unitOfWork.SaveChangesAsync();
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("ApplicationName", user.ApplicationName),
                new Claim("Token", token)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Store token in session
            HttpContext.Session.SetString("AuthToken", token);
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            _logger.LogInformation("User {Username} logged in successfully", user.Username);

            return RedirectToLocal(model.ReturnUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Username}", model.Username);
            ModelState.AddModelError(string.Empty, "Error al iniciar sesión. Por favor, intente nuevamente.");
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var token = HttpContext.Session.GetString("AuthToken");
        if (!string.IsNullOrEmpty(token))
        {
            var appToken = await _authService.ValidateTokenAsync(token);
            if (appToken != null)
            {
                appToken.Revoke();
                var unitOfWork = HttpContext.RequestServices.GetRequiredService<Wms.Domain.Repositories.IUnitOfWork>();
                await unitOfWork.AppTokens.UpdateAsync(appToken);
                await unitOfWork.SaveChangesAsync();
            }
        }

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();

        return RedirectToAction(nameof(Login));
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Dashboard");
    }
}

