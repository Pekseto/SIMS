using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repository;
using Image = Tourist_Project.Domain.Models.Image;

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
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();

        public CreateTour()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourPointRepository = new TourPointRepository();
            imageRepository = new ImageRepository();
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            tour = new Tour();
            InitializeCitiesAndCountries();
        }

        public void CreateClick(object sender, RoutedEventArgs e)
        {
            if (tour.TourPoints.Count() >= 2)
            {
                Image image = new Image(url.Text);
                imageRepository.Save(image);
                tour = new Tour(Name.Text, locationRepository.GetId(city.Text, country.Text), description.Text, language.Text, Convert.ToInt32(maxGuestsNumber.Text), Convert.ToDateTime(startTime.Text), Convert.ToInt32(duration.Text), image.Id);
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
            TourPoint tourPoint = new TourPoint(checkpoint.Text, tourRepository.NextId());
            tourPointRepository.Save(tourPoint);

            if (!string.IsNullOrWhiteSpace(checkpoint.Text))
            {
                tour.TourPoints.Add(tourPoint);
                checkpoint.Clear();
            }
        }

        public void AddImageClick(object sender, RoutedEventArgs e)
        {
            Image image = new Image(url.Text);
            imageRepository.Save(image);

            if (!string.IsNullOrWhiteSpace(url.Text))
            {
                url.Clear();
            }
        }
        private void CountryDropDownClosed(object sender, EventArgs e)
        {
            Cities.Clear();
            foreach (var location in Locations)
            {
                if (location.Country.Equals(country.Text))
                    Cities.Add(location.City);
            }
        }
        private void CityDropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in Locations)
            {
                if (location.City.Equals(city.Text))
                    country.Text = location.Country;
            }
        }
        private static void InitializeCitiesAndCountries()
        {
            foreach (var location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                    Countries.Add(location.Country);
            }
        }
    }
}
