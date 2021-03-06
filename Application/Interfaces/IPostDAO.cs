using Entities.Models;

namespace Application.Interfaces;

public interface IPostDao
{
    public Task<ICollection<Post>?> GetAsync();
    public Task<Post?> GetById(int id);
    public Task<ICollection<Post>> GetPostsByAuthorId(int authorId);
    public Task<Post?> AddAsync(Post post);
    public Task<Post?> DeleteAsync(int postId);
    public Task<Post?> UpdateAsync(Post post);
}