using Application.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.EfcDataAccess;

public class PostSqliteDao : IPostDao
{
    private ForumDbContext _context;
    public PostSqliteDao(ForumDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Post>?> GetAsync()
    {
        ICollection<Post> posts = await _context.Posts.ToListAsync();
        return posts;
    }

    public async Task<Post?> GetById(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<ICollection<Post>> GetPostsByAuthorId(int authorId)
    {
        return _context.Posts.Where(p => p.Author.Id == authorId).ToList();
    }

    public async Task<Post?> AddAsync(Post post)
    {
        EntityEntry<Post> added = _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post?> DeleteAsync(int postId)
    {
        Post? p = await _context.Posts.FindAsync(postId);
        if (p is null)
        {
            throw new Exception($"Could not find Todo with id {postId}. Nothing was deleted");
        }

        _context.Posts.Remove(p);
        await _context.SaveChangesAsync();
        return p;
    }

    public async Task<Post?> UpdateAsync(Post post)
    {
        EntityEntry<Post> update = _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return update.Entity;
    }
}