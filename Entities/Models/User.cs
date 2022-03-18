using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{
    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public User() { }

    [Required, MaxLength(128)] 
    public string UserName { get; set; }
    
    [Required, MinLength(4)] 
    public string Password { get; set; }
}