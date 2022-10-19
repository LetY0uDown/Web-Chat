using Models;
using System.Windows;
using System.Windows.Controls;

namespace Chat_Client.Core.Tools;

internal sealed class MessageDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        Page? page = container as Page;

        return item switch
        {
            ServerMessage => (page!.FindResource("ServerMessageTemplate") as DataTemplate)!,
            UserMessage => (page!.FindResource("UserMessageTemplate") as DataTemplate)!,
            _ => null!
        };
    }
}