using Chat_Client.Core.Tools;
using Chat_Client.Views.Windows;
using WPF_Client_Library;

namespace Chat_Client.ViewModels;

internal sealed class LoginViewModel : ObservableObject
{
    public LoginViewModel()
    {
        LoginCommand = new(async o => {
            App.CurrentUser = new(Login!, Password!);

            await DataProvider.PostAsync(App.CurrentUser!, "ChatData");

            App.ChangeMainWindow(new ChatWindow());

        }, b => !string.IsNullOrWhiteSpace(Login) 
                && !string.IsNullOrWhiteSpace(Password));
    }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public bool HasAccount { get; set; }

    public Command? LoginCommand { get; }
}