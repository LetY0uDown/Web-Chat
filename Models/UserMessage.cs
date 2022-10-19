using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table(nameof(UserMessage))]
public class UserMessage : Entity, IMessage
{
    public UserMessage(string text, User sender)
    {
        Text = text;
        Sender = sender;

        Date = DateTime.Now.ToShortTimeString();
    }

    public string? Text { get; set; }

    public User? Sender { get; set; }

    public string Date { get; set; }
}