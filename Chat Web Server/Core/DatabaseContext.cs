using Microsoft.EntityFrameworkCore;
using Models;

namespace Chat_Web_Server.Core;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<IMessage> Messages => Set<IMessage>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(AppSettings.ConnectionString, ServerVersion.AutoDetect(AppSettings.ConnectionString));
    }
}