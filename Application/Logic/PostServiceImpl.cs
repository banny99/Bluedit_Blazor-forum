using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Application.Logic;

public class PostServiceImpl : IPostService
{
    private IPostDao PostDao { get; set; }

    public PostServiceImpl(IPostDao postDao)
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

    public Task<Post> GetByAuthor(User author)
    {
        return PostDao.GetByAuthor(author);
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
}