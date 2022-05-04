using Entities.Interfaces;
using Entities.Models;
using FileData.JsonDataAccess;

namespace JsonDataAccess.Context;

public class UserJsonDao : IUserDao
{
    
    private JsonContext _jsonContext;
    public UserJsonDao(JsonContext jsonContext)
    {
        _jsonContext = jsonContext;
    }
    
    public async Task<ICollection<User?>> GetAsync()
    {
        return _jsonContext.Forum.Users;
    }

    public async Task<User> GetById(int id)
    {
        return _jsonContext.Forum.Users.First(u => u.Id == id)!;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return _jsonContext.Forum.Users.First(u => u.UserName.Equals(username));
    }

    public async Task<User> AddAsync(User u)
    {
        int largestId = -1;
        if (_jsonContext.Forum.Posts.Any())
        {
            largestId = _jsonContext.Forum.Users.Max(p => p.Id);
        }
        
        int nextId = largestId + 1;
        u.Id = nextId;
        _jsonContext.Forum.Users.Add(u);
        await _jsonContext.SaveChangesAsync();
        return u;
    }

    public async Task<User?> DeleteAsync(int userId)
    {
        User userToRemove = _jsonContext.Forum.Users.First(u => u.Id==userId);
        //delete posts of deleted user
        ICollection<Post> posts = _jsonContext.Forum.Posts.Where(p => p.Author.Id == userId).ToList();
        foreach (var post in posts)
        {
            //delete comments of each deleted post;
            ICollection<Comment> postsComments = _jsonContext.Forum.Comments.Where(c => c.Post.Id == post.Id).ToList();
            foreach (var postsComment in postsComments)
            {
                _jsonContext.Forum.Comments.Remove(postsComment);
            }
            _jsonContext.Forum.Posts.Remove(post);
        }
        //delete comments of deleted user
        ICollection<Comment> usersComments = _jsonContext.Forum.Comments.Where(c => c.WrittenBy!.Id == userId).ToList();
        foreach (var usersComment in usersComments)
        {
            _jsonContext.Forum.Comments.Remove(usersComment);
        }

        _jsonContext.Forum.Users.Remove(userToRemove);
        await _jsonContext.SaveChangesAsync();
        return userToRemove;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        User? userToUpdate = _jsonContext.Forum.Users.First(u => u.Equals(user));
        userToUpdate.Password = user.Password;
        await _jsonContext.SaveChangesAsync();
        return userToUpdate;
    }
}