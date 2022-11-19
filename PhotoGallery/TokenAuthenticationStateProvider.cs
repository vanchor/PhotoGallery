using Microsoft.AspNetCore.Components.Authorization;
using PhotoGallery.Pages;
using PhotoGallery.Infrastructure;
using System.Security.Claims;

internal class TokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public TokenAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        AuthenticationState CreateAnonymous()
        {
            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            return new AuthenticationState(anonymousPrincipal);
        };

        var token = await _localStorageService.GetAsync<SecurityToken>(nameof(SecurityToken));
        if (token == null)
            return CreateAnonymous();

        if (string.IsNullOrWhiteSpace(token.AccessToken) || token.ExpiredAt < DateTime.UtcNow)
            return CreateAnonymous();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, token.Username)
        };

        var identity = new ClaimsIdentity(claims, "Token");
        var anonymousPrincipal = new ClaimsPrincipal(identity);
        return new AuthenticationState(anonymousPrincipal);
    }
}