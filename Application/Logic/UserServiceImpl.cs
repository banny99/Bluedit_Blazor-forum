using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Application.Logic;

public class UserServiceImpl : IUserService
{
    private readonly IUserDao _userDao;

    public UserServiceImpl(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<ICollection<User?>> GetAsync()
    {
        return await _userDao.GetAsync();
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
        if (user == null)
            throw new Exception("Enter required fields please!");
        
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

        return await _userDao.AddAsync(user);
    }

    public Task<User?> DeleteAsync(int userId)
    {
        return _userDao.DeleteAsync(userId);
    }

    public Task<User?> UpdateAsync(User user)
    {
        return _userDao.UpdateAsync(user);
    }
}