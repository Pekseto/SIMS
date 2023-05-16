using System.Windows;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : Window
    {
        public ScheduleRenovation()
        {
            InitializeComponent();
            DataContext = new ScheduleRenovationViewModel();
        }
    }
}
