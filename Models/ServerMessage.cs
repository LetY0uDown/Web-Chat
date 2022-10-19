using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table(nameof(ServerMessage))]
public class ServerMessage : Entity, IMessage
{
    public ServerMessage(string text)
    {
        Text = text;
    }

    public string? Text { get; set; }    
}