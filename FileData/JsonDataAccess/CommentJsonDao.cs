using Entities.Interfaces;
using Entities.Models;

namespace FileData.JsonDataAccess;

public class CommentJsonDao : ICommentDao
{
    private JsonContext _jsonContext;

    public CommentJsonDao(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }


    public async Task<ICollection<Comment>?> GetAllComments()
    {
        Forum f = _jsonContext.Forum;
        return f.Comments;
        // return _jsonContext.Forum.Comments;
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        Comment c = _jsonContext.Forum.Comments.First(c => commentId == c.CommentId);
        return c;
    }

    public async Task<ICollection<Comment>> GetCommentsByPostId(int postId)
    {
        return _jsonContext.Forum.Comments.Where(c => c.Post.Id == postId).ToList();
    }

    public async Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId)
    {
        return _jsonContext.Forum.Comments.Where(c => c.WrittenBy.Id == authorId).ToList();
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        int largestId = -1;
        if (_jsonContext.Forum.Comments.Any()) {
            largestId = _jsonContext.Forum.Comments.Max(p => p.CommentId);
        }
        
        comment.CommentId = ++largestId;
        _jsonContext.Forum.Comments.Add(comment);
        await _jsonContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> DeleteCommentById(int commentId)
    {
        Comment deletedComment = _jsonContext.Forum.Comments.First(c => c.CommentId == commentId);
        _jsonContext.Forum.Comments.Remove(deletedComment);
        await _jsonContext.SaveChangesAsync();
        return deletedComment;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId)
    {
        ICollection<Comment> deletedComments = _jsonContext.Forum.Comments.Where(c => c.WrittenBy!.Id == authorId).ToList();
        foreach (var comment in deletedComments)
        {
            _jsonContext.Forum.Comments.Remove(comment);
        }
        await _jsonContext.SaveChangesAsync();
        return deletedComments;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByPostId(int postId)
    {
        ICollection<Comment> deletedComments = _jsonContext.Forum.Comments.Where(c => c.Post.Id == postId).ToList();
        foreach (var comment in deletedComments)
        {
            _jsonContext.Forum.Comments.Remove(comment);
        }
        await _jsonContext.SaveChangesAsync();
        return deletedComments;
    }
}