using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuideShowWindow.xaml
    /// </summary>
    public partial class GuideShowWindow : Window
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourRepository tourRepository;
        public static bool Live { get; set; }

        public GuideShowWindow()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            TodayTours = new ObservableCollection<Tour>(tourRepository.GetTodaysTours());
            Live = false;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var createTourWindow = new CreateTour();
            createTourWindow.Show();
        }

        private void StartTourClick(object sender, RoutedEventArgs e)
        {
            if (!SelectedTour.Guided && !Live)
            {
                Live = true;

                var tourLiveWindow = new TourLiveWindow(SelectedTour);
                tourLiveWindow.ShowDialog();

                tourRepository.Update(SelectedTour);
            }
            else
            {
                MessageBox.Show("Tour is already guided or one is already active");
            }
        }
    }
}
