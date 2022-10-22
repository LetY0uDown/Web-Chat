using Chat_Client.Core.Tools;
using Chat_Client.Views.Windows;
using System.Windows;
using WPF_Client_Library;

namespace Chat_Client.ViewModels;

internal sealed class LoginViewModel : ObservableObject
{
    public LoginViewModel()
    {
        LoginCommand = new(async o => {
            App.CurrentUser = new(Login!, Password!);

            var controller = HasAccount ? "login" : "registration";

            var result = await DataProvider.PostAsync(App.CurrentUser!, controller);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                App.ChangeMainWindow(new ChatWindow());
            else
                MessageBox.Show($"Login failed with code {result.StatusCode}", "Fail");

        }, b => !string.IsNullOrWhiteSpace(Login) 
                && !string.IsNullOrWhiteSpace(Password));
    }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public bool HasAccount { get; set; }

    public Command? LoginCommand { get; }
}