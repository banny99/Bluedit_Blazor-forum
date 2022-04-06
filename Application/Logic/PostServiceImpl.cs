using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Application.Logic;

public class PostServiceImpl : IPostService
{
    private IPostDao _postDao;

    public PostServiceImpl(IPostDao postDao)
    {
        _postDao = postDao;
    }

    
    public Task<ICollection<Post>?> GetAsync()
    {
        return _postDao.GetAsync();
    }

    public Task<Post> GetById(int id)
    {
        return _postDao.GetById(id);
    }

    public Task<ICollection<Post>> GetPostsByAuthorId(int authorId)
    {
        return _postDao.GetPostsByAuthorId(authorId);
    }

    public Task<Post> AddAsync(Post post)
    {
        return _postDao.AddAsync(post);
    }

    public Task<Post> DeleteAsync(int id)
    {
        return _postDao.DeleteAsync(id);
    }

    public Task<Post> UpdateAsync(Post post)
    {
        return _postDao.UpdateAsync(post);
    }
    
}