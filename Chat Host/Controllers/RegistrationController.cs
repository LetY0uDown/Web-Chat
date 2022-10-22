using Chat_Host.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Chat_Host.Controllers;

[ApiController, Route("[controller]")]
public sealed class RegistrationController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public RegistrationController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult> PostUser([FromBody] User user)
    {
        if (_dbContext.Users.Any(u => u.Username == user.Username))
            return BadRequest();

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
