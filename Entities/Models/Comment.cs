namespace Entities.Models;

public class Comment
{
    public int CommentId { get; set; }
    public User WrittenBy { get; set; }
    public Post Post { get; set; }
    public string Text { get; set; }
    


    public Comment()
    {
    }

    public Comment(int commentId, User writtenBy, Post post, string text)
    {
        CommentId = commentId;
        WrittenBy = writtenBy;
        Post = post;
        Text = text;
    }
}