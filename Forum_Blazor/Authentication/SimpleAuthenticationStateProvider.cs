using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Forum_Blazor.Authentication;

public class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    
    private readonly IAuthService _authService;

    public SimpleAuthenticationStateProvider(IAuthService authService)
    {
        _authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await _authService.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
    
}