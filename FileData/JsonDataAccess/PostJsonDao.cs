using Entities.Interfaces;
using Entities.Models;
using FileData.JsonDataAccess;

namespace JsonDataAccess.Context;

public class PostJsonDao : IPostDao
{

    private JsonContext _jsonContext;
    public PostJsonDao(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }
    
    public async Task<ICollection<Post>?> GetAsync()
    {
        Forum f = _jsonContext.Forum;
        return f.Posts;
        // return _jsonContext.Forum.Posts;
    }

    public async Task<Post> GetById(int id)
    {
        return _jsonContext.Forum.Posts.First(p => p.Id == id);
    }

    public async Task<Post> GetByAuthor(int authorId) 
    { 
        return _jsonContext.Forum.Posts.First(p => p.Author!.Id==authorId); 
    }

    public async Task<Post> AddAsync(Post post)
    {
        int largestId = -1;
        if (_jsonContext.Forum.Posts.Any())
        {
            largestId = _jsonContext.Forum.Posts.Max(p => p.Id);
        }
        
        int nextId = largestId + 1;
        post.Id = nextId;
        _jsonContext.Forum.Posts.Add(post);
        await _jsonContext.SaveChangesAsync();
        return post;
    }

    public async Task<Post> DeleteAsync(int id)
    {
        Post toDelete = _jsonContext.Forum.Posts.First(p => p.Id == id);
        _jsonContext.Forum.Posts.Remove(toDelete);
        await _jsonContext.SaveChangesAsync();
        return toDelete;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        Post toUpdate = _jsonContext.Forum.Posts.First(p => p.Id == post.Id);
        toUpdate.Update(post);
        await _jsonContext.SaveChangesAsync();
        return toUpdate;
    }
    
}