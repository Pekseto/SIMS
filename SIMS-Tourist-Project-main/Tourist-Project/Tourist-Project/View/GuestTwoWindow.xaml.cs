using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuestTwoWindow.xaml
    /// </summary>
    public partial class GuestTwoWindow : Window
    {

        private readonly TourRepository tourRepository;
        private readonly LocationRepository locationRepository;
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public string SelectedCountry { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public string SelectedCity { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public int Duration { get; set; }
        public int GuestsNumber { get; set; }
        public User LoggedInUser { get; set; }
        public GuestTwoWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;

            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();

            Tours = new ObservableCollection<Tour>(tourRepository.GetAll());
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();

            foreach (Location location in locationRepository.GetAll().GroupBy(x => x.Country).Select(y => y.First()))
            {
                Countries.Add(location.Country);
            }

            foreach(Tour tour in tourRepository.GetAll().GroupBy(x => x.Language).Select(y => y.First()))
            {
                Languages.Add(tour.Language);
            }
        }

        private void ShowAllClick(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            foreach(Tour tour in tourRepository.GetAll())
            {
                Tours.Add(tour);
            }
        }

        private void CountriesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cities.Clear();
            foreach(Location location in locationRepository.GetAll())
            {
                if (location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            foreach(Tour tour in tourRepository.GetAll())
            {
                //FILTRIRANJE
                if(SelectedCountry != null && GetLocation(tour).Country != SelectedCountry)
                {
                    continue;
                }

                if(SelectedCity != null && GetLocation(tour).City != SelectedCity)
                {
                    continue;
                }

                if(Duration != 0 && tour.Duration != Duration)
                {
                    continue;
                }

                if(SelectedLanguage != null && tour.Language!= SelectedLanguage)
                {
                    continue;
                }

                if(GuestsNumber!= 0 && tour.MaxGuestsNumber < GuestsNumber)
                {
                    continue;
                }

                Tours.Add(tour);
            }
        }

        private Location GetLocation(Tour tour)
        {
            return locationRepository.GetAll().Find(x => x.Id == tour.LocationId);
        }
    }
}
