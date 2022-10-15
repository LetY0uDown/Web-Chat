namespace Chat_Web_Server.Core;

internal sealed class AppSettings
{
    private readonly IConfiguration _configuration;

    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;

        ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
    }

    internal static string ConnectionString { get; private set; }
}