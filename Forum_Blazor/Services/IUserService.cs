using Entities.Models;

namespace Entities.Interfaces;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
    public Task CreateUserAsync(User newUser);
    public Task UpdateUserAsync(string username, User updatedUser);
    public Task DeleteUserAsync(string username);
}