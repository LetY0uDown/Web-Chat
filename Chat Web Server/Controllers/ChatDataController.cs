using Chat_Web_Server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using System.Collections.ObjectModel;

namespace Chat_Web_Server.Controllers;

[ApiController, Route("chat/[controller]")]
public sealed class ChatDataController : ControllerBase
{
    private readonly DatabaseContext _dbContext;
    private readonly ChatHub _hub;

    public ChatDataController(DatabaseContext dbContext, IHubContext<ChatHub> hubContext)
    {
        _dbContext = dbContext;
        _hub = hubContext as ChatHub;
    }

    [HttpGet("/users")]
    public ActionResult<ObservableCollection<User>> GetUsers()
    {
        return Ok(_dbContext.Users);
    }

    [HttpGet("/messages")]
    public ActionResult<ObservableCollection<IMessage>> GetMessages()
    {
        return Ok(_dbContext.Messages);
    }

    [HttpPost]
    public async void PostMessage([FromBody] UserMessage msg)
    {
        await _hub.Clients.All.SendAsync("Recieve", msg);

        await _dbContext.Messages.AddAsync(msg);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPost]
    public async void PostUser([FromBody] User user)
    {
        await _hub.Clients.AllExcept(_hub.Context.ConnectionId).SendAsync("RecieveConnection", user);
        await _hub.Clients.All.SendAsync("Recieve", new ServerMessage($"{user.Username} подключился к чату"));

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async void DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        await _hub.Clients.AllExcept(_hub.Context.ConnectionId).SendAsync("RecieveConnection", user);
        await _hub.Clients.All.SendAsync("Recieve", new ServerMessage($"{user.Username} вышел из чата"));

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}