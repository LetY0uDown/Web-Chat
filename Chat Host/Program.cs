using Chat_Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.MapGet("/data/messages", (DatabaseContext db) => {
    return db.Messages;
});

app.MapGet("data/users", (DatabaseContext db) => {
    return db.Users;
});

app.Run();