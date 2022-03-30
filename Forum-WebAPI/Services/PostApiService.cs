using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Forum_WebAPI.Services;

public class PostApiService : IPostService
{
    private IPostDao PostDao { get; set; }

    public PostApiService(IPostDao postDao)
    {
        PostDao = postDao;
    }

    
    public Task<ICollection<Post>> GetAsync()
    {
        return PostDao.GetAsync();
    }

    public Task<Post> GetById(int id)
    {
        return PostDao.GetById(id);
    }

    public Task<Post> GetByAuthor(int authorId)
    {
        return PostDao.GetByAuthor(authorId);
    }

    public Task<Post> AddAsync(Post post)
    {
        return PostDao.AddAsync(post);
    }

    public Task<Post> DeleteAsync(int id)
    {
        return PostDao.DeleteAsync(id);
    }

    public Task<Post> UpdateAsync(Post post)
    {
        return PostDao.UpdateAsync(post);
    }

    public void AddComment(Post post, Comment comment)
    {
        PostDao.AddComment(post, comment);
    }
}