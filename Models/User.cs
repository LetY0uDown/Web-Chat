using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table(nameof(User))]
public class User : Entity
{
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string? Username { get; set; }

    public string? Password { get; set; }
}