using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{

    public User(int id, string userName, string password, string role, int securityLevel, int birthYear)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
        BirthYear = birthYear;
    }

    public User() { }

    public User(string userName)
    {
        UserName = userName;
    }


    [Required]
    public int Id { get; set; }

    [Required, MaxLength(128)] 
    public string UserName { get; set; }
    
    [Required, MinLength(4)] 
    public string Password { get; set; }
    
    public string Role { get;  set; }
    public int SecurityLevel { get;  set; }
    public int BirthYear { get; set; }
}