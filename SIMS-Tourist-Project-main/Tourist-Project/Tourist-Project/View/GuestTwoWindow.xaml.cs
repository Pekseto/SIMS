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
        private TourRepository tourRepository;
        private LocationRepository locationRepository;
        public ObservableCollection<Tour> tours { get; set; }
        public Tour selectedTour { get; set; }
        public ObservableCollection<string> countries { get; set; }
        public string selectedCountry { get; set; }
        public ObservableCollection<string> cities { get; set; }
        public string selectedCity { get; set; }
        public ObservableCollection<string> languages { get; set; }
        public string selectedLanguage { get; set; }
        public int duration { get; set; }
        public int numOfPeople { get; set; }
        public GuestTwoWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();

            tours = new ObservableCollection<Tour>(tourRepository.GetAll());

            countries = new ObservableCollection<string>();
            cities = new ObservableCollection<string>();
            foreach(Location location in locationRepository.GetAll().GroupBy(x => x.Country).Select(y => y.First()))
            {
                countries.Add(location.Country);
            }

            languages = new ObservableCollection<string>();
            foreach(Tour tour in tourRepository.GetAll().GroupBy(x => x.Language).Select(y => y.First()))
            {
                languages.Add(tour.Language);
            }
        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            tours.Clear();
            foreach(Tour tour in tourRepository.GetAll())
            {
                tours.Add(tour);
            }
        }

        private void CountriesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities.Clear();
            foreach(Location location in locationRepository.GetAll())
            {
                if (location.Country == selectedCountry)
                {
                    cities.Add(location.City);
                }
            }
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            tours.Clear();
            foreach(Tour tour in tourRepository.GetAll())
            {
                if(selectedCountry != null && GetLocation(tour).Country != selectedCountry)
                {
                    continue;
                }

                if(selectedCity != null && GetLocation(tour).City != selectedCity)
                {
                    continue;
                }

                if(duration != 0 && tour.Duration!= duration)
                {
                    continue;
                }

                if(selectedLanguage != null && tour.Language!= selectedLanguage)
                {
                    continue;
                }

                if(numOfPeople!= 0 && tour.MaxGuestsNumber != numOfPeople)
                {
                    continue;
                }

                tours.Add(tour);
            }
        }

        private Location GetLocation(Tour tour)
        {
            return locationRepository.GetAll().Find(x => x.Id == tour.LocationId);
        }
    }
}
