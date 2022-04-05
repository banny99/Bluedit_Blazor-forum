namespace Entities.Models;

public class Comment
{
    public User? WrittenBy { get; set; }
    public string Text { get; set; }


    public Comment()
    {
        Text = "";
    }
    
    public Comment(User writtenBy, string text)
    {
        WrittenBy = writtenBy;
        Text = text;
    }
}