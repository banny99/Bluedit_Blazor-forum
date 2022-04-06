using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Post
{
    
    public int Id { get; set; }
    public User Author { get; set; }
    
    [Required (ErrorMessage = "Header is required!")]
    public string Header { get; set; }
    
    [Required (ErrorMessage = "Body cannot be empty!")]
    public string Body { get; set; }
    


    public Post() { }

    public Post(User author, string header, string body)
    {
        Author = author;
        Header = header;
        Body = body;
    }


    public void Update(Post toPost)
    {
        this.Id = toPost.Id;
        this.Author = toPost.Author;
        this.Header = toPost.Header;
        this.Body = toPost.Body;
    }
    
}