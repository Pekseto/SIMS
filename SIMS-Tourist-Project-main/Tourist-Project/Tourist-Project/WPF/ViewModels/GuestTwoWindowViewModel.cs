using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using System.Windows.Input;
using Tourist_Project.WPF.Views;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestTwoWindowViewModel : INotifyPropertyChanged
    {
        #region Services
        private readonly TourService tourService;
        private readonly LocationService locationService;
        private readonly TourReservationService reservationService;
        private readonly VoucherService voucherService;
        private readonly TourAttendanceService attendanceService;
        #endregion
        #region Fields
        public User LoggedInUser { get; set; }
        private ObservableCollection<TourDTO> tours;
        public ObservableCollection<TourDTO> Tours
        {
            get { return tours; }
            set
            {
                if (value != tours)
                {
                    tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }
        public TourDTO SelectedTour { get; set; }
        public ObservableCollection<string> Countries { get; set; }

        private string selectedCountry;
        public string SelectedCountry
        {
            get => selectedCountry;
            set
            {
                if(selectedCountry != value)
                {
                    selectedCountry = value;
                    LoadCitiesComboBox();
                }
            }
        }
        private ObservableCollection<string> cities;
        public ObservableCollection<string> Cities
        {
            get { return cities; }
            set
            {
                if(cities != value)
                {
                    cities = value;
                    OnPropertyChanged(nameof(Cities));
                }
            }
        }
        public string SelectedCity { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public Voucher SelectedVoucher { get; set; }

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
                    duration = int.Parse(value);
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
                    numberOfPeople = int.Parse(value);
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
                    guestsNumber = int.Parse(value);
                }
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand ShowAllCommand { get; set; }
        public ICommand ReserveCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        #endregion

        public GuestTwoWindowViewModel(User user)
        {
            LoggedInUser = user;

            SearchCommand = new RelayCommand(OnSearchClick);
            ShowAllCommand = new RelayCommand(OnShowAllClick);
            ReserveCommand = new RelayCommand(OnReserveClick);
            HistoryCommand = new RelayCommand(OnHistoryClick);

            tourService = new TourService();
            locationService = new LocationService();
            reservationService = new TourReservationService();
            voucherService = new VoucherService();
            attendanceService = new TourAttendanceService();

            ShowNotifications();

            Tours = new ObservableCollection<TourDTO>(tourService.GetAllAvailableToursDTO());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>(tourService.GetAllLanguages());
            Vouchers = new ObservableCollection<Voucher>(voucherService.GetAllForUser(user.Id));

            voucherService.DeleteInvalidVouchers(LoggedInUser.Id);
        }

        private void ShowNotifications()
        {
            foreach(TourAttendance tourAttendance in attendanceService.GetAll())
            {
                if(tourAttendance.UserId == LoggedInUser.Id && tourAttendance.Presence == Presence.Pending)
                {
                    if (MessageBox.Show("The guide has called you out for " + tourService.GetAll().Find(t => t.Id == tourAttendance.TourId) + "\nPlease confirm your presence!".ToString(),"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        tourAttendance.Presence = Presence.Yes;
                        attendanceService.Update(tourAttendance);
                    }
                    else
                    {
                        tourAttendance.Presence = Presence.No;
                        attendanceService.Update(tourAttendance);
                    }
                }
            }
        }

        private void OnHistoryClick()
        {
            var HistoryWindow = new TourHistoryView(LoggedInUser);
            HistoryWindow.Show();
        }

        private void OnReserveClick()
        {
            if (guestsNumber > 0 && SelectedTour != null)
            {
                int tourCapacityLeft = SelectedTour.SpotsLeft;

                if (tourCapacityLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    DisplaySimilarTours(SelectedTour);
                }
                else if (tourCapacityLeft < guestsNumber)
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourCapacityLeft.ToString());
                }
                else
                {
                    SelectedTour.SpotsLeft -= guestsNumber;
                    var tourReservation = new TourReservation(LoggedInUser.Id, SelectedTour.Id, guestsNumber, SelectedVoucher != null ? true : false);
                    reservationService.Save(tourReservation);
                    var tourAttendance = new TourAttendance(LoggedInUser.Id, SelectedTour.Id);
                    attendanceService.Save(tourAttendance);

                    if (SelectedVoucher != null)
                    {
                        voucherService.Delete(SelectedVoucher.Id);
                        Vouchers.Remove(SelectedVoucher);
                    }

                    MessageBox.Show("Reservation is successful");                  
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number and select a tour\n" +
                                "in order to make a reservation!");
            }
        }

        private void OnShowAllClick()
        {
            Tours = new ObservableCollection<TourDTO>(tourService.GetAllAvailableToursDTO());
        }

        private void OnSearchClick()
        {
            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tourDTO in tourService.GetAllAvailableToursDTO())
            {
                //FILTERING
                if (SelectedCountry != null && tourDTO.Location.Country != SelectedCountry)
                {
                    continue;
                }
                if (SelectedCity != null && tourDTO.Location.City != SelectedCity)
                {
                    continue;
                }
                if (duration != 0 && tourDTO.Duration != duration)
                {
                    continue;
                }
                if (SelectedLanguage != null && tourDTO.Language != SelectedLanguage)
                {
                    continue;
                }
                if (numberOfPeople != 0 && tourDTO.SpotsLeft < numberOfPeople)
                {
                    continue;
                }

                filteredList.Add(tourDTO);
            }
            Tours = filteredList;
        }

        private void LoadCitiesComboBox()
        {
            Cities = locationService.GetCitiesFromCountry(SelectedCountry);
        }

        private void DisplaySimilarTours(TourDTO selectedTour)
        {
            int locationId = selectedTour.LocationId;
            int selectedTourId = selectedTour.Id;

            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in tourService.GetAllAvailableToursDTO())
            {
                if (tour.LocationId == locationId && tour.Id != selectedTourId)
                {
                    filteredList.Add(tour);
                }
            }
            Tours = filteredList;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
