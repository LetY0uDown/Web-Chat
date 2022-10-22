namespace Models;

public class Message : Entity
{
    public string? Sender { get; set; }

    public string? Date { get; set; }

    public string? Text { get; set; }
}