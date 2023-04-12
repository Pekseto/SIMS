using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for RateGuestWindow.xaml
    /// </summary>
    public partial class RateGuestWindow : Window
    {
        public RateGuestWindow(Notification notification)
        {
            InitializeComponent();
            DataContext = new RateGuestViewModel(notification, this);
        }
    }
}
