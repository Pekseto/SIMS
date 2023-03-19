using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourist_Project.DTO;
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
        private readonly TourReservationRepository reservationRepository;
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourDTO> Tours { get; set; }
        public TourDTO SelectedTour { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public string SelectedCountry { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public string SelectedCity { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }

        private readonly Regex numberRegex = new Regex("^[0-9]+$");
        private int duration;
        public string Duration
        {
            get { return duration.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    duration = Int32.Parse(value);
                }
            }
        }

        private int numberOfPeople;
        public string NumberOfPeople
        {
            get { return numberOfPeople.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    numberOfPeople = Int32.Parse(value);
                }
            }
        }

        private int guestsNumber;
        public string GuestsNumber
        {
            get { return guestsNumber.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    guestsNumber = Int32.Parse(value);
                }
            }
        }
        public GuestTwoWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;

            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            reservationRepository = new TourReservationRepository();

            Tours = new ObservableCollection<TourDTO>();
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();

            foreach (Tour tour in tourRepository.GetAll())
            {
                var tourDTO = new TourDTO(tour)
                {
                    SpotsLeft = GetLeftoverSpots(tour),
                    Location = GetLocation(tour)
                };
                Tours.Add(tourDTO);
            }

            foreach (Location location in locationRepository.GetAll().GroupBy(x => x.Country).Select(y => y.First()))
            {
                Countries.Add(location.Country);
            }

            foreach (Tour tour in tourRepository.GetAll().GroupBy(x => x.Language).Select(y => y.First()))
            {
                Languages.Add(tour.Language);
            }
        }

        private void ShowAllClick(object sender, RoutedEventArgs e)
        {
            toursDataGrid.ItemsSource = Tours;
        }

        private void CountriesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cities.Clear();
            foreach (Location location in locationRepository.GetAll())
            {
                if (location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tourDTO in Tours)
            {
<<<<<<< HEAD
                //FILTERING
                if(SelectedCountry != null && tourDTO.Location.Country != SelectedCountry)
=======
                //FILTRIRANJE
                if (SelectedCountry != null && tourDTO.Location.Country != SelectedCountry)
>>>>>>> feat/accommodationView
                {
                    continue;
                }

                if (SelectedCity != null && tourDTO.Location.City != SelectedCity)
                {
                    continue;
                }

<<<<<<< HEAD
                if(duration != 0 && tourDTO.Duration != duration)
=======
                if (Duration != 0 && tourDTO.Duration != Duration)
>>>>>>> feat/accommodationView
                {
                    continue;
                }

                if (SelectedLanguage != null && tourDTO.Language != SelectedLanguage)
                {
                    continue;
                }

<<<<<<< HEAD
                if(numberOfPeople != 0 && tourDTO.MaxGuestsNumber < numberOfPeople)
=======
                if (NumberOfPeople != 0 && tourDTO.MaxGuestsNumber < NumberOfPeople)
>>>>>>> feat/accommodationView
                {
                    continue;
                }

                filteredList.Add(tourDTO);
            }
            toursDataGrid.ItemsSource = filteredList;
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if(guestsNumber > 0 && SelectedTour != null)
=======
            if (GuestsNumber > 0 && SelectedTour != null)
>>>>>>> feat/accommodationView
            {
                int tourCapacityLeft = SelectedTour.SpotsLeft;

                if (tourCapacityLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    DisplaySimilarTours(SelectedTour);
                }
<<<<<<< HEAD
                else if(tourCapacityLeft < guestsNumber)
=======
                else if (tourCapacityLeft < GuestsNumber)
>>>>>>> feat/accommodationView
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourCapacityLeft.ToString());
                }
                else
                {
                    SelectedTour.SpotsLeft -= guestsNumber;
                    var tourReservation = new TourReservation(LoggedInUser.Id, SelectedTour.Id, guestsNumber);
                    reservationRepository.Save(tourReservation);
                    MessageBox.Show("Reservation is successful");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number and select a tour\n" +
                                "in order to make a reservation!");
            }
        }

        private void DisplaySimilarTours(TourDTO selectedTour)
        {
            int locationId = selectedTour.LocationId;
            int selectedTourId = selectedTour.Id;

            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in Tours)
            {
                if (tour.LocationId == locationId && tour.Id != selectedTourId)
                {
                    filteredList.Add(tour);
                }
            }
            toursDataGrid.ItemsSource = filteredList;
        }

        private int GetLeftoverSpots(Tour tour)
        {
            int retVal = tour.MaxGuestsNumber;
            foreach (TourReservation reservation in reservationRepository.GetAll())
            {
                if (reservation.TourId == tour.Id)
                {
                    retVal -= reservation.GuestsNumber;
                }
            }
            return retVal;
        }
        private Location GetLocation(Tour tour)
        {
            return locationRepository.GetAll().Find(x => x.Id == tour.LocationId);
        }
    }
}
