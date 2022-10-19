using System.Windows;

namespace Chat_Client.Views.Windows;

public partial class ChatWindow : Window
{
    public ChatWindow()
    {
        InitializeComponent();

        usernameText.Text = $"Вы: {App.CurrentUser!.Username}";
    }
}