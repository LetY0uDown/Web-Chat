using Microsoft.EntityFrameworkCore;
using Models;

namespace Chat_Host.Services;

public sealed class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    private readonly string _connectionString;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;

        _connectionString = _configuration["ConnectionStrings:DefaultConnection"];

        Database.EnsureCreated();
    }

    public DbSet<Message> Messages => Set<Message>();

    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        base.OnConfiguring(optionsBuilder);
    }
}