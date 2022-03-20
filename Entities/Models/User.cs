using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{

    public User(string userName, string password, string role, int securityLevel, int birthYear)
    {
        UserName = userName;
        Password = password;
        Role = role;
        Role = "none";
        SecurityLevel = securityLevel;
        BirthYear = birthYear;
    }

    public User() { }
    

    [Required, MaxLength(128)] 
    public string UserName { get; set; }
    
    [Required, MinLength(4)] 
    public string Password { get; set; }
    
    public string Role { get;  set; }
    public int SecurityLevel { get;  set; }
    public int BirthYear { get; set; }
}