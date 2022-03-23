using Entities.Models;

namespace Contracts.Services;

public interface IPostService
{
    public Task<ICollection<Post>> GetAsync();
    public Task<Post> GetById(int id);
    public Task<Post> GetByAuthor(User author);
    public Task<Post> AddAsync(Post post);
    public Task<Post> DeleteAsync(int id);
    public Task<Post> UpdateAsync(Post post);
}