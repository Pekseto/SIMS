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
using Tourist_Project.DTO;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuestOne.xaml
    /// </summary>
    public partial class GuestOne : Window
    {

        public ObservableCollection<AccommodationDTO> SearchResults { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }
        public List<AccommodationDTO> AccommodationDTOs{ get; set; }

        public AccommodationDTO selectedAccommodationDTO { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public ObservableCollection<Tourist_Project.Model.Image> Images { get; set; }

        public Location Location { get; set; }
        public ObservableCollection<Location> Locations { get; set; }

        public ObservableCollection<string> FullLocations { get; set;}

        private readonly AccommodationRepository accommodationRepository;

        private readonly LocationRepository locationRepository;

        private readonly ImageRepository imageRepository;

        private readonly AccommodationDTORepository accommodationDTORepository;
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Tourist_Project.Model.AccommodationType AccomodationType { get; set; }
        public string SearchedName { get; set; }
        public string SearchedLocation { get; set; }

        public int SearchedNumberOfGuests { get; set; }

        public int SearchedDaysBeforeCancelation { get; set; }
        public string SelectedType { get; set; }

        public ObservableCollection<string> AccommodationTypes { get; set; }

        public User LoggedInUser { get; set; }
        public GuestOne(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;

            locationRepository = new LocationRepository();
            accommodationRepository = new AccommodationRepository();
            imageRepository = new ImageRepository();
            accommodationDTORepository = new AccommodationDTORepository();    
            
            AccommodationDTOs = new List<AccommodationDTO>();


            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            AccommodationTypes = new ObservableCollection<string>();
            SearchResults = new ObservableCollection<AccommodationDTO>(AccommodationDTOs);
            FullLocations = new ObservableCollection<string>();
            

            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Tourist_Project.Model.Image>(imageRepository.GetAll());

            AccommodationDTOs = accommodationDTORepository.createDTOs(Accommodations, Locations, Images);


            Countries = GetCountries();
            Cities = GetCities();
            AccommodationTypes = GetAccommodationTypes();          
            FullLocations = GetFullLocationNames(); 
        }

        public ObservableCollection<string> GetAccommodationTypes()
        {
            foreach (string type in Enum.GetNames(typeof(Tourist_Project.Model.AccommodationType)))
            {
                type.ToString();
                AccommodationTypes.Add(type);
            }
            return AccommodationTypes;
        }

        public ObservableCollection<string> GetFullLocationNames()
        {
            foreach(AccommodationDTO accommodationDTO in AccommodationDTOs)
            {
                FullLocations.Add(accommodationDTO.LocationFullName);
            }
            return FullLocations;
        }
        public ObservableCollection<string> GetCountries()
        {
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
            return Countries;
        }
    
        public ObservableCollection<string> GetCities()
        {
            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
            }
            return Cities;
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


        public void HandleEmptyStrings()
        {
            if (SearchedName == null)
            {
                SearchedName = string.Empty;
            }

            if (SelectedCountry == null)
            {
                SelectedCountry = string.Empty;
            }

            if (SelectedCity == null)
            {
                SelectedCity = string.Empty;
            }

            if (SearchedNumberOfGuests == null)
            {
                SearchedNumberOfGuests = 1;
            }

            if (SearchedDaysBeforeCancelation == null)
            {
                foreach (Accommodation accommodation in Accommodations)
                {
                    SearchedDaysBeforeCancelation = accommodation.DaysBeforeCancel;
                }
            }
        }
  

        public void SearchClick(object sender, RoutedEventArgs e)
        {
            //AccommodationDTOs.Clear();
            SearchResults.Clear();

            HandleEmptyStrings();

            foreach (AccommodationDTO accommodationDTO in AccommodationDTOs)
            {
                if (SearchedName != null && accommodationDTO.Name != SearchedName)
                {
                    continue;
                }

                if(SelectedCountry + SelectedCity != null && accommodationDTO.LocationFullName != SelectedCity + " " + SelectedCountry)
                {
                    continue;
                }

                if( SelectedType != null && accommodationDTO.AccommodationType.ToString() != SelectedType)
                {
                    continue;
                }
                if (SearchedDaysBeforeCancelation != null && accommodationDTO.DaysBeforeCancel < SearchedDaysBeforeCancelation)
                {
                    continue;
                }
                if (SearchedNumberOfGuests != null && accommodationDTO.MaxGuestNum < SearchedNumberOfGuests)
                {
                    continue;
                }

                SearchResults.Add(accommodationDTO);
            }
            DataGrid.ItemsSource = SearchResults;
        }

        public void ShowAllClick(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = AccommodationDTOs;
        }

        public void ReserveClick(object sender, RoutedEventArgs e)
        {
            var BookAccommodation = new BookAccommodationWindow();
            BookAccommodation.Show();
        }
    }
}
