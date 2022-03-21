using Entities.Interfaces;
using Entities.Models;
using FileData.JsonDataAccess;

namespace JsonDataAccess.Context;

public class UserJsonDAO : IUserDao
{
    
    private JsonContext _jsonContext;
    public UserJsonDAO(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }
    
    public async Task<ICollection<User>> GetAsync()
    {
        return _jsonContext.Forum.Users;
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsername(string username)
    {
        return Task.FromResult(_jsonContext.Forum.Users.First(u => u.UserName.Equals(username)));
    }

    public async Task AddAsync(User u)
    {
        _jsonContext.Forum.Users.Add(u);
        await _jsonContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string username)
    {
        _jsonContext.Forum.Users.Remove(_jsonContext.Forum.Users.First(u => u.UserName.Equals(username)));
        await _jsonContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        User userToUpdate = _jsonContext.Forum.Users.First(u => u.Equals(user));
        userToUpdate.Password = user.Password;
        await _jsonContext.SaveChangesAsync();
    }
}