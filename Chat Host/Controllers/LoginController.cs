using Chat_Host.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Chat_Host.Controllers;

[ApiController, Route("[controller]")]
public sealed class LoginController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public LoginController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult ValidateUser([FromBody] User user)
    {
        var findedUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);

        if (findedUser is null)
            return NotFound();

        if (findedUser.Password != user.Password)
            return Unauthorized();

        return Ok();
    }
}
