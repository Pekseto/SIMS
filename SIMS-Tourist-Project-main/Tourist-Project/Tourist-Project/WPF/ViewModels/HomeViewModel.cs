using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;

namespace Tourist_Project.WPF.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly TourService tourService;
        private readonly LocationService locationService;

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
                if (selectedCountry != value)
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
                if (cities != value)
                {
                    cities = value;
                    OnPropertyChanged(nameof(Cities));
                }
            }
        }
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

        private object currentViewModel;
        public object CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ShowAllCommand { get; set; }
        public ICommand ReserveCommand { get; set; }

        public HomeViewModel(User user, object currentViewModel)
        {
            LoggedInUser = user;
            CurrentViewModel = currentViewModel;

            SearchCommand = new RelayCommand(OnSearchClick);
            ShowAllCommand = new RelayCommand(OnShowAllClick);
            ReserveCommand = new RelayCommand(OnReserveClick);

            tourService = new TourService();
            locationService = new LocationService();

            ShowNotifications();

            Tours = new ObservableCollection<TourDTO>(tourService.GetAllAvailableToursDTO());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>(tourService.GetAllLanguages());

            //voucherService.DeleteInvalidVouchers();

        }

        private void OnReserveClick()
        {
            CurrentViewModel = new TourReservationViewModel(LoggedInUser, SelectedTour);
        }

        private void ShowNotifications()
        {
            /*foreach (TourAttendance tourAttendance in attendanceService.GetAll())
            {
                if (tourAttendance.UserId == LoggedInUser.Id && tourAttendance.Presence == Presence.Pending)
                {
                    if (MessageBox.Show("The guide has called you out for " + tourService.GetAll().Find(t => t.Id == tourAttendance.TourId) + "\nPlease confirm your presence!".ToString(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
            }*/
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
