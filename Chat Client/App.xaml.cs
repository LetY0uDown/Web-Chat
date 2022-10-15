using System.Windows;

namespace Chat_Client;

public partial class App : Application
{
    internal static void ChangeMainWindow(Window newWindow)
    {
        Current.MainWindow.Hide();

        Current.MainWindow = newWindow;

        Current.MainWindow.Show();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {

    }
}