using Chat_Client.Core.Tools;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System.Collections.ObjectModel;
using WPF_Client_Library;

namespace Chat_Client.ViewModels;

internal sealed class ChatViewModel : ObservableObject
{
    private HubConnection? _hubConnection;

    public ChatViewModel()
    {
        InitData();

        Connect();

        _hubConnection!.On<IMessage>("Recieve", msg => {
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
            var msg = new UserMessage(Message!, App.CurrentUser!);

            await DataProvider.PostAsync(msg, "ChatData");

            Messages.Add(msg);
            Message = string.Empty;

        }, b => !string.IsNullOrWhiteSpace(Message));
    }

    public string? Message { get; set; }

    public ObservableCollection<IMessage>? Messages { get; set; } = new();

    public ObservableCollection<User>? Users { get; set; } = new();

    public Command SendMessageCommand { get; private init; }

    private async void InitData()
    {
        Messages = await DataProvider.GetAsync<ObservableCollection<IMessage>>("ChatData", "/messages");
        Users = await DataProvider.GetAsync<ObservableCollection<User>>("ChatData", "/users");
    }

    private async void Connect()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl(Config.GetValue("host"))
                                                   .WithAutomaticReconnect()
                                                   .Build();        

        await _hubConnection.StartAsync();
    }
}