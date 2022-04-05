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
    
    public async Task<ICollection<User?>> GetAsync()
    {
        return _jsonContext.Forum.Users;
    }

    public async Task<User> GetById(int id)
    {
        return _jsonContext.Forum.Users.First(u => u.Id == id)!;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return _jsonContext.Forum.Users.First(u => u.UserName.Equals(username));
    }

    public async Task<User?> AddAsync(User? u)
    {
        _jsonContext.Forum.Users.Add(u);
        await _jsonContext.SaveChangesAsync();
        return u;
    }

    public async Task<User?> DeleteAsync(string username)
    {
        User? userToRemove = _jsonContext.Forum.Users.First(u => u.UserName.Equals(username));
        _jsonContext.Forum.Users.Remove(userToRemove);
        await _jsonContext.SaveChangesAsync();
        return userToRemove;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        User? userToUpdate = _jsonContext.Forum.Users.First(u => u.Equals(user));
        userToUpdate.Password = user.Password;
        await _jsonContext.SaveChangesAsync();
        return userToUpdate;
    }
}