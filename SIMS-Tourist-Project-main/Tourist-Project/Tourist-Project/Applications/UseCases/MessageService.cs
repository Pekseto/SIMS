using System.Windows;

namespace Tourist_Project.Applications.UseCases
{
    public class MessageService
    {
        public bool ShowDismissalDialog(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
    }
}
