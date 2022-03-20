using System.Security.Claims;
using System.Text.Json;
using Entities.Interfaces;
using Entities.Models;
using Microsoft.JSInterop;

namespace Forum_Blazor.Authentication
{
    public class AuthServiceImpl : IAuthService
    {

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
        // private readonly IUserService userService;
        private IUserDAO UserDao;
        private readonly IJSRuntime jsRuntime;
        private ClaimsPrincipal principal;

        public AuthServiceImpl(IUserDAO userDao, IJSRuntime jsRuntime)
        {
            // this.userService = userService;
            UserDao = userDao;
            this.jsRuntime = jsRuntime;
        }

        public async Task LoginAsync(string username, string password)
        {
            // User? user = await userService.GetUserAsync(username);
            User? user = await UserDao.GetByUsername(username);

            ValidateLoginCredentials(password, user);

            await CacheUserAsync(user);

            principal = CreateClaimsPrincipal(user);

            OnAuthStateChanged?.Invoke(principal);
        }

        public async Task LogoutAsync()
        {
            await ClearUserFromCacheAsync();
            principal = CreateClaimsPrincipal(null);
            OnAuthStateChanged?.Invoke(principal);
        }

        public async Task<ClaimsPrincipal> GetAuthAsync()
        {
            if (principal != null)
            {
                return principal;
            }

            string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (string.IsNullOrEmpty(userAsJson))
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            User? user = JsonSerializer.Deserialize<User>(userAsJson);
            // user = await userService.GetUserAsync(user.UserName);
            user = await UserDao.GetByUsername(user.UserName);
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
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
        }
        private async Task<User?> GetUserFromCacheAsync()
        {
            string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
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
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
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
