using Entities.Models;

namespace Entities.Interfaces;

public interface IPostDAO
{
    public Task<ICollection<Post>> GetAsync();
    public Task<Post> GetById(int id);
    public Task<Post> GetByAuthor(User author);
    public Task<Post> AddAsync(Post post);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Post post);
}