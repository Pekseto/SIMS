using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for TourLiveWindow.xaml
    /// </summary>
    public partial class TourLiveWindow : Window
    {
        public static ObservableCollection<TourPoint> TourPoints { get; set; }
        public TourPoint SelectedTourPoint { get; set; }
        private Tour selectedTour;
        private TourPointRepository tourPointRepository;

        public TourLiveWindow(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;

            this.selectedTour = selectedTour;
            tourPointRepository = new TourPointRepository();
            TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(this.selectedTour.Id));
            TourPoints[0].Visited = true;
            tourPointRepository.Update(TourPoints[0]);
        }

        public void EarlyEndClick(object sender, RoutedEventArgs e)
        {
            selectedTour.Guided = true;
            GuideShowWindow.Live = false;
            Close();
        }

        public void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            SelectedTourPoint.Visited = true;
            UpdateCollection();

            if (IsAllChecked(TourPoints))
            {
                selectedTour.Guided = true;
                GuideShowWindow.Live = false;
                MessageBox.Show("Tour ended");
                Close();
            }
        }

        public void TouristListClick(object sender, RoutedEventArgs e)
        {
            var touristListWindow = new TouristListWindow(SelectedTourPoint);
            touristListWindow.Show();
        }

        public void UpdateCollection()
        {
            tourPointRepository.Update(SelectedTourPoint);
            TourPoints.Clear();
            foreach (TourPoint point in tourPointRepository.GetAllForTour(selectedTour.Id))
            {
                TourPoints.Add(point);
            }
        }

        public bool IsAllChecked(ObservableCollection<TourPoint> tourPoints)
        {
            return tourPoints.ToList().Find(tourPoint => tourPoint.Visited == false) == null;
        }
    }
}
