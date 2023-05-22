using System.Windows;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : Window
    {
        public ScheduleRenovation(AccommodationViewModel accommodation)
        {
            InitializeComponent();
            DataContext = new ScheduleRenovationViewModel(accommodation, this);
        }
    }
}
