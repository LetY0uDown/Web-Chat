using Models;
using System.Windows;
using System.Windows.Controls;

namespace Chat_Client.Core.Tools;

internal sealed class MessageDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        FrameworkElement? list = container as FrameworkElement;
        var msg = item as Message;
        
        return msg!.Sender switch
        {
            "SERVER" => (list!.FindResource("ServerMessageTemplate") as DataTemplate)!,
            _ => (list!.FindResource("UserMessageTemplate") as DataTemplate)!
        };
    }
}