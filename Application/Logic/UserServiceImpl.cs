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

    public Task<ICollection<User>> GetAsync()
    {
        return _userDao.GetAsync();
    }

    public Task<User> GetById(int id)
    {
        return _userDao.GetById(id);
    }

    public Task<User> GetByUsername(string username)
    {
        return _userDao.GetByUsername(username);
    }

    public Task<User> AddAsync(User user)
    {
        return _userDao.AddAsync(user);
    }

    public Task<User> DeleteAsync(string username)
    {
        return _userDao.DeleteAsync(username);
    }

    public Task<User> UpdateAsync(User user)
    {
        return _userDao.UpdateAsync(user);
    }
}