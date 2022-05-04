using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData.EfcDataAccess;

public class ForumDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    
    
    //easy (not-recommended) way of setting this
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = C:\Users\asus\School\Ja-College\VIA\OneDrive - ViaUC\3rd_semester-S22\DNP1\BlueditForum\EfcData\Forum.db");
    }
    
    //The above method is a simple approach, however we have now hardcoded the database info,
    //and it may not be easy to modify. Usually the connection info will go into a settings file,
    //and the program will read from that. It is left to the reader to google how to do that,
    //if they're interested.
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Post>().HasKey(p => p.Id);
        modelBuilder.Entity<Comment>().HasKey(c => c.CommentId);
    }
    
    public void Seed()
    {
        if (!Users.Any())
        {
            User u = new User(0, "uName", "pasS123", "role", 1, 1234);
            Users.Add(u);
            Posts.Add(new Post(u, "header", "body,body ...vbsd"));

            SaveChanges();
        }
    }
    
}