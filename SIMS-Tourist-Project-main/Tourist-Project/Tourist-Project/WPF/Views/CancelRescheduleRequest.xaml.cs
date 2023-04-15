using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for CancelRescheduleRequest.xaml
    /// </summary>
    public partial class CancelRescheduleRequest : Window
    {
        public CancelRescheduleRequest(RescheduleRequest rescheduleRequest)
        {
            InitializeComponent();
            DataContext = new CancelRescheduleViewModel(rescheduleRequest, this);
        }
    }
}
