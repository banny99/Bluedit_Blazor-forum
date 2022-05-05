namespace Entities.Models;

public class Comment
{
    public int CommentId { get; set; }
    public User WrittenBy { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }



    public Comment()
    {
    }

    public Comment(int commentId, User writtenBy,int postId, string text)
    {
        CommentId = commentId;
        WrittenBy = writtenBy;
        PostId = postId;
        Text = text;
    }
}