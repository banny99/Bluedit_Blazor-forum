using Entities.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.EfcDataAccess;

public class CommentSqliteDao : ICommentDao
{
    private ForumDbContext _context;
    public CommentSqliteDao(ForumDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Comment>?> GetAllComments()
    {
        ICollection<Comment> comments = await _context.Comments
            .Include(c=>c.WrittenBy)
            .Include(c=>c.Post)
            .ToListAsync();
        return comments;
    }

    public async Task<Comment?> GetCommentById(int commentId)
    {
        ICollection<Comment> comments = await _context.Comments
            .Include(c=>c.WrittenBy)
            .Include(c=>c.Post)
            .ToListAsync();

        return comments.First(c => c.CommentId == commentId);
    }

    public async Task<ICollection<Comment>> GetCommentsByPostId(int postId)
    {
        ICollection<Comment> comments = _context.Comments.Where(c => c.Post.Id == postId)
            .Include(c=>c.WrittenBy)
            .Include(c=>c.Post)
            .ToList();
        return comments;
    }

    public async Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId)
    {
        ICollection<Comment> comments = _context.Comments.Where(c => c.WrittenBy.Id == authorId)
            .Include(c=>c.WrittenBy)
            .Include(c=>c.Post)
            .ToList();
        return comments;
    }

    public async Task<Comment?> AddComment(Comment comment)
    {
        _context.Attach(comment.Post);
        // _context.Attach(comment.WrittenBy);
        EntityEntry<Comment> added = _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Comment?> DeleteCommentById(int commentId)
    {
        Comment? c = await _context.Comments.FindAsync(commentId);
        if (c is null)
        {
            throw new Exception($"Could not find Todo with id {commentId}. Nothing was deleted");
        }
        _context.Comments.Remove(c);
        await _context.SaveChangesAsync();
        return c;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId)
    {
        ICollection<Comment> comments = await GetCommentsByAuthorId(authorId);
        foreach (var comment in comments)
        {
            _context.Comments.Remove(comment);
        }

        return comments;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByPostId(int postId)
    {
        ICollection<Comment> comments = await GetCommentsByPostId(postId);
        foreach (var comment in comments)
        {
            _context.Comments.Remove(comment);
        }

        return comments;
    }
}