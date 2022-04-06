namespace Entities.Models;

public class Comment
{
    // public User? WrittenBy { get; set; }
    public string Text { get; set; }

    public int Id { get; set; }
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string Author { get; set; }


    public Comment()
    {
        AuthorId = -1;
        PostId = -1;
        Text = "";
    }
    
    public Comment(User writtenBy, Post post, string text)
    {
        if (writtenBy==null)
        {
            AuthorId = -1;
            Author = "anonym";
        }
        else
        {
            AuthorId = writtenBy.Id;
            Author = writtenBy.UserName;
        }
        
        Text = text;
        PostId = post.Id;
    }
}