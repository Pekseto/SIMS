using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tourist_Project.Repository;
using Tourist_Project.Model;
using Tourist_Project.Observer;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuestOne.xaml
    /// </summary>
    public partial class GuestOne : Window
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public Location Location { get; set; }
        public ObservableCollection<Location> Locations { get; set; }

        private readonly AccommodationRepository accommodationRepository;

        private readonly LocationRepository locationRepository;
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        public AccommodationType AccomodationType { get; set; }
        public string SearchedName { get; set; }
        public string SearchedLocation { get; set; }

        public int SearchedNumberOfGuests { get; set; }

        public int SearchedDaysBeforeCancelation { get; set; }
        public string SelectedType { get; set; }

        public ObservableCollection<string> AccommodationTypes { get; set; }

        public GuestOne()
        {
            InitializeComponent();
            DataContext = this;

            locationRepository = new LocationRepository();
            accommodationRepository = new AccommodationRepository();
            //accommodationRepository.Subscribe(this);//znaci da ce ovde prikazivati sve promene u listama - ovaj prozor je subskrajbovan na kontroler?
            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            
           
            AccommodationTypes = new ObservableCollection<string>();

            Countries = new ObservableCollection<string>();

            Cities = new ObservableCollection<string>();

            foreach (Location location in Locations)
            {
                if (Countries.Contains(location.Country))
                {
                    continue;
                }
                else
                {
                    Countries.Add(location.Country);
                }
            }

            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
            }
          

            foreach(string type in Enum.GetNames(typeof(AccommodationType)))
                {
                    type.ToString();
                    AccommodationTypes.Add(type);
                }

        }

    
        private void SelectedCountryChanged(object sender, SelectionChangedEventArgs e)
        {

            Cities.Clear();
            foreach(Location location in Locations)
            {
                if(location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void SelectedCityChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (Location location in Locations)
            {
                if (location.City == SelectedCity)
                {
                    SelectedCountry = location.Country;  
                }
            }
        }
          
        public ObservableCollection<Accommodation> SearchClick(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();
            if(SearchedName == null)
            {
                SearchedName = string.Empty;
            }
            
            if(SelectedCountry == null)
            {
                SelectedCountry = string.Empty;
            }

            if(SelectedCity == null)
            {
                SelectedCity = string.Empty;
            }    



            return Accommodations;
        }

    }
}
