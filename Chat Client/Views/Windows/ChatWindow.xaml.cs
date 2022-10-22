using System.Windows;
using Chat_Client.ViewModels;

namespace Chat_Client.Views.Windows;

public partial class ChatWindow : Window
{
    public ChatWindow()
    {
        InitializeComponent();

        usernameText.Text = $"Вы: {App.CurrentUser!.Username}";
    }

    private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var viewModel = DataContext as ChatViewModel;
        await viewModel!.Disconnect();
    }
}
