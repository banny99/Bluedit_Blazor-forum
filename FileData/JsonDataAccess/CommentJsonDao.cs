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


    public async Task<ICollection<Comment>> GetAllComments()
    {
        return _jsonContext.Forum.Comments;
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        Comment c = _jsonContext.Forum.Comments.First(c => commentId == c.Id);
        return c;
    }

    public async Task<ICollection<Comment>> GetCommentsByPostId(int postId)
    {
        return _jsonContext.Forum.Comments.Where(c => c.PostId == postId).ToList();
    }

    public async Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId)
    {
        return (ICollection<Comment>) _jsonContext.Forum.Comments.Where(c => c.AuthorId == authorId);
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        int largestId = -1;
        if (_jsonContext.Forum.Comments.Any()) {
            largestId = _jsonContext.Forum.Comments.Max(p => p.Id);
        }
        
        comment.Id = ++largestId;
        _jsonContext.Forum.Comments.Add(comment);
        await _jsonContext.SaveChangesAsync();
        return comment;
    }
}