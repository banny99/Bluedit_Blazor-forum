using Entities.Models;

namespace Contracts.Services;

public interface ICommentService
{
    public Task<ICollection<Comment>> GetAllComments();
    public Task<Comment> GetCommentById(int commentId);
    public Task<ICollection<Comment>> GetCommentsByPostId(int postId);
    public Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId);
    public Task<Comment> AddComment(Comment comment);
    public Task<Comment> DeleteCommentById(int commentId);
    public Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId);
    Task<ICollection<Comment>> DeleteCommentsByPostId(int postId);
}