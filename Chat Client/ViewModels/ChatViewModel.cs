using Chat_Client.Core.Tools;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPF_Client_Library;

namespace Chat_Client.ViewModels;

internal sealed class ChatViewModel : ObservableObject
{
    private HubConnection? _hubConnection;

    public ChatViewModel()
    {
        _ = InitData();

        _ = Connect();

        _hubConnection!.On<Message>("Recieve", msg => {
            Messages.Add(msg);
        });

        _hubConnection!.On<User>("RecieveConnection", user => {
            if (Users.Contains(user))
                Users.Remove(user);
            else
                Users.Add(user);
        });

        SendMessageCommand = new(async o =>
        {
            Message? msg = new()
            {
                Text = Message,
                Date = System.DateTime.Now.ToShortTimeString(),
                Sender = App.CurrentUser!.Username
            };

            await _hubConnection!.InvokeAsync("SendMessage", msg);

            Message = string.Empty;

        }, b => !string.IsNullOrWhiteSpace(Message));
    }

    public string? Message { get; set; }

    public ObservableCollection<Message>? Messages { get; set; } = new();

    public ObservableCollection<User>? Users { get; set; } = new();

    public Command SendMessageCommand { get; private init; }

    internal async Task Disconnect()
    {
        await _hubConnection!.InvokeAsync("Disconnect", App.CurrentUser!);
        await _hubConnection!.StopAsync();
    }

    private async Task InitData()
    {
        Messages = await DataProvider.GetAsync<ObservableCollection<Message>>("data", "/messages");
        var users = await DataProvider.GetAsync<ObservableCollection<User>>("data", "/users");

        Users = new(users.Where(u => u.Username != App.CurrentUser!.Username));
    }

    private async Task Connect()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl(Config.GetValue("host") + "chat")
                                                   .WithAutomaticReconnect()
                                                   .Build();

        await _hubConnection.StartAsync();

        await _hubConnection.InvokeAsync("Connect", App.CurrentUser!);
    }    
}