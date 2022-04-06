using Entities.Models;

namespace Entities.Interfaces;

public interface IPostDao
{
    public Task<ICollection<Post>?> GetAsync();
    public Task<Post> GetById(int id);
    public Task<ICollection<Post>> GetPostsByAuthorId(int authorId);
    public Task<Post> AddAsync(Post post);
    public Task<Post> DeleteAsync(int id);
    public Task<Post> UpdateAsync(Post post);
}