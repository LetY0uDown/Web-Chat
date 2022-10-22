using Chat_Web_Server.Core;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.ObjectModel;

namespace Chat_Web_Server.Controllers;

[ApiController, Route("data")]
public sealed class ChatDataController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public ChatDataController(DatabaseContext dbContext)
    {
        Console.WriteLine("controller is active");
        _dbContext = dbContext;
    }

    [HttpGet("/users")]
    public ActionResult<ObservableCollection<User>> GetUsers()
    {
        return Ok(_dbContext.Users);
    }

    [HttpGet("/messages")]
    public ActionResult<ObservableCollection<Message>> GetMessages()
    {
        return Ok(_dbContext.UsersMessages);
    }
        
    [HttpPost]
    public async void PostUser([FromBody] User user)
    {
        //await _hub.Clients.AllExcept(_hub.Context.ConnectionId).SendAsync("RecieveConnection", user);
        //await _hub.Clients.All.SendAsync("Recieve", new ServerMessage($"{user.Username} подключился к чату"));

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async void DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        //await _hub.Clients.AllExcept(_hub.Context.ConnectionId).SendAsync("RecieveConnection", user);
        //await _hub.Clients.All.SendAsync("Recieve", new ServerMessage($"{user.Username} вышел из чата"));

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}