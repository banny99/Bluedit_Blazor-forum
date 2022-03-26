namespace Entities.Models;

public class Comment
{
    public User? WrittenBy { get; set; }
    public string? Text { get; set; }

    
    public Comment(){}
    
    public Comment(User writtenBy, string text)
    {
        WrittenBy = writtenBy;
        text = text;
    }
}