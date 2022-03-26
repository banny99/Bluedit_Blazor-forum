using Entities.Models;

namespace Entities.Interfaces;

public interface IUserDao
{
    public Task<ICollection<User?>> GetAsync();
    public Task<User> GetById(int id);
    public Task<User?> GetByUsername(string username);
    public Task<User?> AddAsync(User? user);
    public Task<User?> DeleteAsync(string username);
    public Task<User?> UpdateAsync(User user);
}