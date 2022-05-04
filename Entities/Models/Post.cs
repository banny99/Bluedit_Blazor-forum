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
    public List<Comment> Comments { get; set; }



    public Post() { }

    public Post(User author, string header, string body)
    {
        Author = author;
        Header = header;
        Body = body;
        Comments = new List<Comment>();
    }


    public void Update(Post toPost)
    {
        Id = toPost.Id;
        Author = toPost.Author;
        Header = toPost.Header;
        Body = toPost.Body;
    }
    
}