using Microsoft.AspNetCore.SignalR;
using Models;

namespace Chat_Host.Services;

public sealed class ChatHub : Hub
{
    private const string SERVER = "SERVER";

    private readonly DatabaseContext _dbContext;

    private static readonly List<User> _onlineUsers = new();

    public ChatHub(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteMessage(int id)
    {
        var msg = await _dbContext.Messages.FindAsync(id);

        _dbContext.Messages.Remove(msg!);
        await _dbContext.SaveChangesAsync();

        await Clients.All.SendAsync("DeleteMessage", msg);
    }

    public async Task SendMessage(Message message)
    {
        var msg = await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();

        await Clients.All.SendAsync("RecieveMessage", msg.Entity);
    }

    public async Task Disconnect(User user)
    {
        _onlineUsers.Remove(user);

        await Clients.Others.SendAsync("HandleDisconnection", user);

        var msg = new Message
        {
            Sender = SERVER,
            Date = DateTime.Now.ToShortTimeString(),
            Text = $"{user.Username} вышел из чата"
        };

        await SendMessage(msg);
    }

    public async Task Connect(User user)
    {
        await Clients.Others.SendAsync("HandleConnection", user);

        await Clients.Caller.SendAsync("GetOnlineUsers", _onlineUsers);

        _onlineUsers.Add(user);

        var msg = new Message
        {
            Sender = SERVER,
            Date = DateTime.Now.ToShortTimeString(),
            Text = $"{user.Username} присоеденился к чату"
        };

        await SendMessage(msg);
    }
}
