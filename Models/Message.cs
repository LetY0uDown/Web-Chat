using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table(nameof(Message))]
public class Message : Entity
{
    public Message(string text, User sender)
    {
        Text = text;
        Sender = sender;

        Date = DateTime.Now;
    }

    public string? Text { get; set; }

    public User? Sender { get; set; }

    public DateTime Date { get; set; }
}