using System.Security.Claims;
using System.Text.Json;
using Contracts.Services;
using Entities.Models;
using Microsoft.JSInterop;

namespace Forum_Blazor.Authentication
{
    public class AuthServiceImpl : IAuthService
    {

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

        public User GetLoggedUser()
        {
            return LoggedUser;
        }

        // private readonly IUserService userService;
        private IUserService _userService;
        private readonly IJSRuntime _jsRuntime;
        private User LoggedUser { get; set; }
        private ClaimsPrincipal principal;

        public AuthServiceImpl(IUserService userService, IJSRuntime jsRuntime)
        {
            _userService = userService;
            _jsRuntime = jsRuntime;
        }

        public async Task LoginAsync(string username, string password)
        {
            // User? user = await userService.GetUserAsync(username);
            LoggedUser = await _userService.GetByUsername(username);

            ValidateLoginCredentials(password, LoggedUser);

            await CacheUserAsync(LoggedUser);

            principal = CreateClaimsPrincipal(LoggedUser);

            OnAuthStateChanged?.Invoke(principal);
        }

        public async Task LogoutAsync()
        {
            await ClearUserFromCacheAsync();
            principal = CreateClaimsPrincipal(null);
            OnAuthStateChanged.Invoke(principal);
        }

        public async Task<ClaimsPrincipal> GetAuthAsync()
        {
            if (principal != null)
            {
                return principal;
            }

            string userAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (string.IsNullOrEmpty(userAsJson))
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            User? user = JsonSerializer.Deserialize<User>(userAsJson);
            // user = await userService.GetUserAsync(user.UserName);
            user = await _userService.GetByUsername(user.UserName);
            principal = CreateClaimsPrincipal(user);
            return principal;
        }

        private void ValidateLoginCredentials(string password, User? user)
        {
            if (user == null)
            {
                throw new Exception("Username not found");
            }

            if (!password.Equals(user.Password))
            {
                throw new Exception("Password incorrect");
            }
        }

        private async Task CacheUserAsync(User? user)
        {
            string serialisedData = JsonSerializer.Serialize(user);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
        }
        private async Task<User?> GetUserFromCacheAsync()
        {
            string userAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (string.IsNullOrEmpty(userAsJson)) return null;
            User user = JsonSerializer.Deserialize<User>(userAsJson)!;
            return user;
        }

        private ClaimsPrincipal CreateClaimsPrincipal(User? user)
        {
            ClaimsIdentity identity = new();
            if (user != null)
            {
                identity = ConvertUserToClaimsIdentity(user);
            }

            ClaimsPrincipal principal = new(identity);

            return principal;
        }

        private async Task ClearUserFromCacheAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        }

        private ClaimsIdentity ConvertUserToClaimsIdentity(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Role", user.Role),
                new Claim("SecurityLevel", user.SecurityLevel.ToString()),
                new Claim("BirthYear", user.BirthYear.ToString())
            };

            return new ClaimsIdentity(claims, "apiauth_type");
        }

    }
}
