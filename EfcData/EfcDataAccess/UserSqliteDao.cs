using Entities.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.EfcDataAccess;

public class UserSqliteDao : IUserDao
{
    private readonly ForumDbContext _context;
    public UserSqliteDao(ForumDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<User>> GetAsync() 
    {
        ICollection<User> users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return _context.Users.First(u => u.UserName.Equals(username));
    }

    public async Task<User?> AddAsync(User user)
    {
        EntityEntry<User> added = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<User?> DeleteAsync(int userId)
    {
        User? u = await _context.Users.FindAsync(userId);
        if (u is null)
        {
            throw new Exception($"Could not find Todo with id {userId}. Nothing was deleted");
        }

        _context.Users.Remove(u);
        await _context.SaveChangesAsync();
        return u;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        EntityEntry<User> updated = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }
    
    //return await todos.Where(t=>
    //  (t.Id==id && t.IsCompleted==isCompleted) ||
    //  (t.Id==id) ||
    //  (t.IsCompleted==isCompleted)
    //).ToListAsync();
}