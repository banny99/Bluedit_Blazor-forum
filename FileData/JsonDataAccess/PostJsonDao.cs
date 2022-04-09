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
        Forum f = _jsonContext.Forum;
        return f.Posts.First(p => p.Id == id);
    }

    public async Task<ICollection<Post>> GetPostsByAuthorId(int authorId) 
    { 
        return _jsonContext.Forum.Posts.Where(p => p.Author.Id==authorId).ToList(); 
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

    public async Task<Post> DeleteAsync(int postId)
    {
        Post toDelete = _jsonContext.Forum.Posts.First(p => p.Id == postId);
        
        //+ delete all comments that belonged to that post
        ICollection<Comment> comments = _jsonContext.Forum.Comments.Where(c => c.PostId == postId).ToList();
        foreach (var comment in comments)
        {
            _jsonContext.Forum.Comments.Remove(comment);
        }
        //remove post
        _jsonContext.Forum.Posts.Remove(toDelete);
        //save
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