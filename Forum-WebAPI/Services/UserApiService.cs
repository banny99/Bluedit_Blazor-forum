using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Forum_WebAPI.Services;

public class UserApiService : IUserService
{
    private readonly IUserDao _userDao;

    public UserApiService(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public Task<ICollection<User?>> GetAsync()
    {
        return _userDao.GetAsync();
    }

    public Task<User> GetById(int id)
    {
        return _userDao.GetById(id);
    }

    public Task<User?> GetByUsername(string username)
    {
        return _userDao.GetByUsername(username);
    }

    public async Task<User?> AddAsync(User? user)
    {
        if (user.Password.Any(char.IsWhiteSpace))
            throw new Exception("Password cannot contain any white-space characters!");
        if (!user.Password.Any(char.IsLetter))
            throw new Exception("Password must contain at least one letter!");
        if (!user.Password.Any(char.IsDigit))
            throw new Exception("Password must contain at least one digit!");
        if (!user.Password.Any(char.IsUpper))
            throw new Exception("Password must contain at least one capital letter!");
        if (!user.Password.Any(char.IsLower))
            throw new Exception("Password must contain at least one lower-cased letter!");

        try
        {
            //if found ->no exception thrown-> user already exists
            User? u = await GetByUsername(user.UserName);
        }
        catch (Exception e)
        {
            return await _userDao.AddAsync(user);
        }
        
        throw new Exception($"User with username: {user.UserName} already exists!!");
    }

    public Task<User?> DeleteAsync(string username)
    {
        return _userDao.DeleteAsync(username);
    }

    public Task<User?> UpdateAsync(User user)
    {
        return _userDao.UpdateAsync(user);
    }
}