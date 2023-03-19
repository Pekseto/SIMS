using System;
using System.Linq;
using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Repository;
using Image = Tourist_Project.Model.Image;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private TourRepository tourRepository;
        private LocationRepository locationRepository;
        private TourPointRepository tourPointRepository;
        private ImageRepository imageRepository;
        private Tour tour;

        public CreateTour()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourPointRepository = new TourPointRepository();
            imageRepository = new ImageRepository();
            tour = new Tour();
        }

        public void CreateClick(object sender, RoutedEventArgs e)
        {
            if (tour.TourPoints.Count() >= 2)
            {
                Image image = new Image(Url.Text);
                imageRepository.Save(image);
                tour = new Tour(Name.Text, locationRepository.GetId(City.Text, Country.Text), Description.Text, Language.Text, Convert.ToInt32(MaxGuestsNumber.Text), Convert.ToDateTime(StartTime.Text), Convert.ToInt32(Duration.Text), image.Id);

                tourRepository.Save(tour);
                if (tour.StartTime.Date == DateTime.Today.Date)
                {
                    GuideShowWindow.TodayTours.Add(tour);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("You must enter minimum two checkpoints!");
            }

        }
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddCheckpointClick(object sender, RoutedEventArgs e)
        {
            TourPoint tourPoint = new TourPoint(Checkpoint.Text, tourRepository.NextId());
            tourPointRepository.Save(tourPoint);

            if (!string.IsNullOrWhiteSpace(Checkpoint.Text))
            {
                tour.TourPoints.Add(tourPoint);
                Checkpoint.Clear();
            }
        }
    }
}
