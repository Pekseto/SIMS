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
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly TourService tourService = new();
        private readonly LocationService locationService = new();

        public User LoggedInUser { get; set; }
        private readonly NavigationStore navigationStore;

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

        private int duration;
        public int Duration
        {
            get { return duration; }
            set
            {
                if(value >= 0)
                {
                    duration = value;
                }
            }
        }

        private int numberOfPeople;
        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            set
            {
                if(value >= 0)
                {
                    numberOfPeople = value;
                }
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ShowAllCommand { get; set; }
        public ICommand ReserveCommand { get; set; }

        public HomeViewModel(User user, NavigationStore navigationStore)
        {
            LoggedInUser = user;
            this.navigationStore = navigationStore;

            Tours = new ObservableCollection<TourDTO>(tourService.GetAllAvailableToursDTO());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>(tourService.GetAllLanguages());

            SearchCommand = new RelayCommand(OnSearchClick);
            ShowAllCommand = new RelayCommand(OnShowAllClick);
            ReserveCommand = new NavigateCommand<TourReservationViewModel>(navigationStore, () => new TourReservationViewModel(user, SelectedTour, this.navigationStore, this), CanReserve);

            ShowNotifications();
        }

        private bool CanReserve()
        {
            return SelectedTour != null;
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
    }
}
