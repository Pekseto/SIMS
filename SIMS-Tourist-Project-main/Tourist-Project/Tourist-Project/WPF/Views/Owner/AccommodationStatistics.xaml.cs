using System.Windows;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationStatistics.xaml
    /// </summary>
    public partial class AccommodationStatistics : Window
    {
        public AccommodationStatistics(AccommodationViewModel accommodationViewModel)
        {
            InitializeComponent();
            DataContext = new AccommodationStatisticsViewModel(accommodationViewModel);
        }
    }
}
