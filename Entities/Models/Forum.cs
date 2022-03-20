namespace Entities.Models;

public class Forum
{
    // Add e.g. ICollection<Post> or ICollection<SubForum> or similar.
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }

    public Forum()
    {
        Users = new List<User>();
        Posts = new List<Post>();
    }

    private ICollection<Post> Seed()
    {
        Post[] ps =
        {
            new Post(new User("Username1", "pass1"), "header1", "body1 ...")
            {
                Id = 1,
            },
            new Post(new User("Username2", "pass2"), "header2", "body2 ...")
            {
                Id = 2,
            },
            new Post(new User("Username3", "pass3"), "header3", "body3 ...")
            {
                Id = 3,
            },
            new Post(new User("Username4", "pass4"), "header4", "body4 ...")
            {
                Id = 4,
            },
            new Post(new User("Username5", "pass5"), "header5", "body5 ...")
            {
                Id = 5,
            },
        };
        return ps.ToList();
    }
}