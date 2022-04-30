using Entities.Models;

namespace Entities.Interfaces;

public interface ICommentDao
{
    public Task<ICollection<Comment>?> GetAllComments();
    public Task<Comment?> GetCommentById(int commentId);
    public Task<ICollection<Comment>> GetCommentsByPostId(int postId);
    public Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId);
    public Task<Comment?> AddComment(Comment comment);
    Task<Comment?> DeleteCommentById(int commentId);
    Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId);
    Task<ICollection<Comment>> DeleteCommentsByPostId(int postId);
}