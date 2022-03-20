using Entities.Models;

namespace Entities.Interfaces;

public interface IUserDAO
{
    public Task<ICollection<User>> GetAsync();
    public Task<User> GetById(int id);
    public Task<User> GetByUsername(string username);
    public Task AddAsync(User user);
    public Task DeleteAsync(string username);
    public Task UpdateAsync(User user);
}