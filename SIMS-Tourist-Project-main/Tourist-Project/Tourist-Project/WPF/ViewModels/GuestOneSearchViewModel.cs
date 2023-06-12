using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Views;
using Tourist_Project.Applications.UseCases;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestOneSearchViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Window _window;
        public DataGrid GuestOneDataGrid { get; set; } = new();
        #region SearchDataDisplay
        public ObservableCollection<String> Countries { get; set; } = new ObservableCollection<String>();
        public ObservableCollection<String> Cities { get; set; } = new ObservableCollection<String>();
        public ObservableCollection<String> AccommodationTypes { get; set; } = new ObservableCollection<String>();
        #endregion

        private String selectedCity;
        public String SelectedCity
        {
            get => selectedCity;
            set
            {
                if (value == selectedCity) return;
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        private String selectedCountry;
        public String SelectedCountry
        {
            get => selectedCountry;
            set
            {
                if (value == selectedCountry) return;
                selectedCity = value;
                OnPropertyChanged("SelectedCountry");
            }
        }

        private LocationRepository _locationRepository = new();
        private AccommodationRepository _accommodationRepository = new();

        private AccommodationService _accommodationService = new();
        private ObservableCollection<AccommodationViewModel> _accommodationsViewModel;

        public ObservableCollection<AccommodationViewModel> AccommodationsViewModel
        {
            get => _accommodationsViewModel;
            set
            {
                if(value == _accommodationsViewModel) return;
                _accommodationsViewModel = value;
                OnPropertyChanged("AccommodationsViewModel");
                
            }
        }

        #region SearchParameters
        public String AccommodationName { get; set; }
        public String SelectedType { get; set; }
        public int SearchedCancelationThreshold { get; set; }
        public int SearchedGuestNum { get; set; }
        public int SearchedStayingDays { get; set; }
        public String SelectedTypeChanged { get; set; }
        public String SelectedCityChanged { get; set; }
        public String SelectedCountryChanged { get; set; }

        

        #endregion
        #region Commands
        public ICommand Search_Command { get; set; }
        public ICommand ShowAll_Command { get; set; }
        public ICommand Close_Command { get; set; }
        #endregion
        public GuestOneSearchViewModel(Window window, DataGrid guestOneDataGrid, ObservableCollection<AccommodationViewModel> accommodationViewModels)
        {
            //_user = user;
            _window = window;
            GuestOneDataGrid = guestOneDataGrid;
            AccommodationsViewModel = accommodationViewModels;

            Countries = GetCountries(); 
            Cities = GetCities();
            AccommodationTypes = GetAccommodationTypes();

            Search_Command = new RelayCommand(SearchLogic, CanSearch);
            Close_Command = new RelayCommand(CloseWindow, CanClose);
            ShowAll_Command = new RelayCommand(ShowAll, CanShowAll);
        }

        #region GettingDataForDisplay
        public ObservableCollection<String> GetCountries()
        {
            foreach (var accommodationViewmodel in AccommodationsViewModel)
            {
                if (Countries.Contains(accommodationViewmodel.Location.Country))
                {
                    continue;
                }
                else
                {
                    Countries.Add(accommodationViewmodel.Location.Country);
                }
            }
            Countries.Add("Any");
            return Countries;
        }

        private ObservableCollection<String> GetCities()
        {
            foreach (var accommodationViewModel in AccommodationsViewModel)
            {
                if (Cities.Contains(accommodationViewModel.Location.City))
                {
                    continue;
                }

                {
                    Cities.Add(accommodationViewModel.Location.City);
                }
            }
            Cities.Add("Any");
            return Cities;
        }


        private ObservableCollection<String> GetAccommodationTypes()
        {
            foreach (var accommodationViewModel in AccommodationsViewModel)
            {
                if (AccommodationTypes.Contains(accommodationViewModel.Accommodation.Type.ToString()))
                {
                    continue;
                }
                {
                    AccommodationTypes.Add(accommodationViewModel.Accommodation.Type.ToString());
                }
            }
            AccommodationTypes.Add("Any");
            return AccommodationTypes;
        }
        #endregion

        #region SearchLogic
        private bool CanSearch()
        {
            return true;
        }



        public void HandleEmptyStrings()
        {
            if (AccommodationName == null)
            {
                AccommodationName = string.Empty;
            }

            if (SelectedCountry == null || SelectedCountry == "Any")
            {
                SelectedCountry = string.Empty;
            }

            if (SelectedCity == null || SelectedCity == "Any")
            {
                SelectedCity = string.Empty;
            }

            if (SearchedGuestNum == 0)
            {
                SearchedGuestNum = 1;
            }

            if (SearchedCancelationThreshold == 0)
            {
                SearchedCancelationThreshold = 0;    
            }

            if (SelectedType == null || SelectedType == "Any")
            {
                SelectedType = string.Empty;
            }

            if(SearchedStayingDays == 0)
            {
                SearchedStayingDays = 1;
            }
        }

        private void SearchLogic()
        {
            //AccommodationsViewModel.Clear();

            ObservableCollection<AccommodationViewModel> searchedViewModels = new ObservableCollection<AccommodationViewModel>();
            HandleEmptyStrings();
            foreach (AccommodationViewModel accommodation in AccommodationsViewModel)
            {
                if (accommodation.Accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) && accommodation.Accommodation.MaxGuestNum >= SearchedGuestNum
                    && accommodation.Accommodation.CancellationThreshold >= SearchedCancelationThreshold &&
                    accommodation.Accommodation.Type.ToString().Contains(SelectedType) &&
                accommodation.Location.ToString().Contains(SelectedCity + ", " + SelectedCountry) && accommodation.Accommodation.MinStayingDays >= SearchedStayingDays)
                {
                   searchedViewModels.Add(accommodation);
                }
            }

            //AccommodationsViewModel = searchedViewModels;
            GuestOneDataGrid.ItemsSource = searchedViewModels;
            _window.Close();
            
            
        }
        #endregion //implementirati do kraja

        #region CloseWindow
        private bool CanClose()
        {
            return true;
        }

        private void CloseWindow()
        {
            //var guestOneWindow = new GuestOneWindow(_user);
            //guestOneWindow.Show();
            _window.Close();
        }
        #endregion

        #region ShowAll

        private bool CanShowAll()
        {
            return true;
        }

        private void ShowAll()
        {
            GuestOneDataGrid.ItemsSource = AccommodationsViewModel;
            _window.Close();
        }


        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
