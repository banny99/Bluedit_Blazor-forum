using Entities.Models;

namespace Entities.Interfaces.Impls;

public class InMemoryUserService : IUserService
{

    private List<User> users = new()
    {
        new User("Troels", "Troels1234", "Teacher", 3, 1986),
        new User("Maria", "oneTwo3FOUR", "Student", 2, 2001),
        new User("Anne", "password", "HeadOfDepartment", 5, 1975)        
    };
    
    
    public async Task<User> GetUserAsync(string username)
    {
        User? find = users.Find(user => user.UserName.Equals(username));
        return find;
    }

    public Task CreateUserAsync(User newUser)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(string username, User updatedUser)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(string username)
    {
        throw new NotImplementedException();
    }
}