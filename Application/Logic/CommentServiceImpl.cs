using Contracts.Services;
using Entities.Interfaces;
using Entities.Models;

namespace Application.Logic;

public class CommentServiceImpl : ICommentService
{
    private ICommentDao _commentDao;
    public CommentServiceImpl(ICommentDao commentDao)
    {
        _commentDao = commentDao;
    }

    
    public Task<ICollection<Comment>> GetAllComments()
    {
        return _commentDao.GetAllComments();
    }

    public Task<Comment> GetCommentById(int commentId)
    {
        return _commentDao.GetCommentById(commentId);
    }

    public Task<ICollection<Comment>> GetCommentsByPostId(int postId)
    {
        return _commentDao.GetCommentsByPostId(postId);
    }

    public Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId)
    {
        return _commentDao.GetCommentsByAuthorId(authorId);
    }

    public Task<Comment> AddComment(Comment comment)
    {
        return _commentDao.AddComment(comment);
    }

    public Task<Comment> DeleteCommentById(int commentId)
    {
        return _commentDao.DeleteCommentById(commentId);
    }

    public Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId)
    {
        return _commentDao.DeleteCommentsByAuthorId(authorId);
    }

    public Task<ICollection<Comment>> DeleteCommentsByPostId(int postId)
    {
        return _commentDao.DeleteCommentsByPostId(postId);
    }
}