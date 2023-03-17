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
        private readonly TourReservationRepository reservationRepository;
        public ObservableCollection<TourDTO> Tours { get; set; }
        public TourDTO SelectedTour { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public string SelectedCountry { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public string SelectedCity { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public int Duration { get; set; }
        public int NumberOfPeople { get; set; }
        public User LoggedInUser { get; set; }
        public int GuestsNumber { get; set; }
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

            foreach(Tour tour in tourRepository.GetAll())
            {
                var tourDTO = new TourDTO(tour)
                {
                    GuestsLeft = GetLeftoverCapacity(tour),
                    Location = GetLocation(tour)
                };
                Tours.Add(tourDTO);
            }

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
                var tourDTO = new TourDTO(tour);
                tourDTO.GuestsLeft = GetLeftoverCapacity(tour);
                tourDTO.Location = GetLocation(tour);
                Tours.Add(tourDTO);
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

                if(NumberOfPeople!= 0 && tour.MaxGuestsNumber < NumberOfPeople)
                {
                    continue;
                }

                var tourDTO = new TourDTO(tour)
                {
                    GuestsLeft = GetLeftoverCapacity(tour),
                    Location = GetLocation(tour)
                };
                Tours.Add(tourDTO);
            }
        }

        private void ReserveClick(object sender, RoutedEventArgs e)
        {
            if(GuestsNumber > 0 && SelectedTour != null)
            {
                int tourCapacityLeft = SelectedTour.GuestsLeft;           

                if(tourCapacityLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    DisplayOtherTours(SelectedTour);
                }
                else if(tourCapacityLeft < GuestsNumber)
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourCapacityLeft.ToString());
                }
                else
                {
                    SelectedTour.GuestsLeft -= GuestsNumber;
                    var tourReservation = new TourReservation(LoggedInUser.Id, SelectedTour.Id, GuestsNumber);
                    reservationRepository.Save(tourReservation);
                    MessageBox.Show("Reservation is successful");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number and select a tour\nin order to make a reservation!");
            }
        }

        private void DisplayOtherTours(TourDTO selectedTour)
        {
            int locationId = selectedTour.LocationId;
            int selectedTourId = selectedTour.Id;

            Tours.Clear();
            foreach(Tour tour in tourRepository.GetAll())
            {
                if(tour.LocationId == locationId && tour.Id != selectedTourId)
                {
                    TourDTO tourDTO = new TourDTO(tour)
                    {
                        GuestsLeft = GetLeftoverCapacity(tour),
                        Location = GetLocation(tour)
                    };
                    Tours.Add(tourDTO);
                }
            }
        }

        private int GetLeftoverCapacity(Tour tour)
        {
            int retVal = tour.MaxGuestsNumber;
            foreach (TourReservation reservation in reservationRepository.GetAll())
            {
                if(reservation.Id == tour.Id)
                {
                    retVal -= reservation.GuestsNumber;
                }
            }
            return retVal;
        }
        private Location? GetLocation(Tour tour)
        {
            return locationRepository.GetAll().Find(x => x.Id == tour.LocationId);
        }
    }
}
