using Microsoft.AspNetCore.SignalR;
using Models;

namespace Chat_Host.Services;

public sealed class ChatHub : Hub
{
    private readonly DatabaseContext _dbContext;

    public ChatHub(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendMessage(Message message)
    {
        await Clients.All.SendAsync("Recieve", message);

        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Disconnect(User user)
    {
        var msg = new Message
        {
            Sender = "SERVER",
            Date = DateTime.Now.ToShortTimeString(),
            Text = $"{user.Username} вышел из чата"
        };

        await SendMessage(msg);
    }

    public async Task Connect(User user)
    {
        await Clients.Others.SendAsync("RecieveConnection", user);

        var msg = new Message
        {
            Sender = "SERVER",
            Date = DateTime.Now.ToShortTimeString(),
            Text = $"{user.Username} присоеденился к чату"
        };

        await SendMessage(msg);
    }
}