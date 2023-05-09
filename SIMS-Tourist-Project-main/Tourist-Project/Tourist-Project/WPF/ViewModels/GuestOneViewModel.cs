using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.DTO;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.WPF.Views;
using Tourist_Project.View;
using System.Windows.Controls;

namespace Tourist_Project.WPF.ViewModels;



public class GuestOneViewModel
{
    //name, locationFullName, MaxGuestNum, CancelationThreshold, MinStayingDays
    private LocationService _locationService = new LocationService();
    private AccommodationService _accommodationService = new AccommodationService();

    #region SearchParameters
    public List<String> Countries { get; set; }
    public List<String> Cities { get; set; }

    public List<String> AccommodationsType { get; set; }

    public String SelectedType { get; set; }

    public int MaxGuestNum { get; set; }
    public int MinStayingDays { get; set; }

    public int SearchedCancelationThreshold { get; set; }
    public String SelectedCountry { get; set; }
    public String SelectedCity { get; set; }
    public Accommodation SelectedAccommodation { get; set; }

    public String AccommodationName { get; set; }
    #endregion

    #region Presentation
    public static ObservableCollection<String> LocationsFullName = new();
    public ObservableCollection<Accommodation> Accommodations { get; set; } = new();
    public ObservableCollection<Accommodation> AccommodationsView { get; set; } = new();

    public List<Location> Locations { get; set; }

    #endregion
    private Window _window;

    public ICommand RateAccommodation_Command { get; set; }
    public ICommand CreateReservation_Command { get; set; }

    public ICommand Search_Command { get; set; }
    public GuestOneViewModel(Window window)
    {
        this._window = window;
        this.DataGrid = dataGrid;
        Locations = new List<Location>();
        Locations = GetLocationsForAccommodations();
        AccommodationsView = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        Countries = _locationService.GetAllCountries();
        Countries.Add("Any");
        Cities = _locationService.GetAllCities();
        Cities.Add("Any");
        AccommodationsType = new List<String>();
        AccommodationsType = GetAccommodationsType();
        Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        GenerateLocations();
        HandleCitiesForCountry();
        RateAccommodation_Command = new RelayCommand(RateAccommodation, CanRate);
        CreateReservation_Command = new RelayCommand(CreateReservation, CanCreateReservation);
        ShowUserReservations_Command = new RelayCommand(ShowReservations, CanShowReservations);
        SearchReservations_Command = new RelayCommand(SearchLogic, CanSearch);
        ShowAll_Command = new RelayCommand(ShowAllReservations, CanShowAll);
    }


    #region LoadingObjectsForDisplay

    public bool CanShowAll()
    {
        return true;
    }

    public void ShowAllReservations()
    {
        DataGrid.ItemsSource = AccommodationsView;
        foreach (Accommodation accommodation in AccommodationsView)
        {
            //accommodation.Location = _locationService.Get(accommodation.LocationId);
        }
    }
    public bool CanSearch()
    {
        return true;
    }
    public bool CanShowReservations()
    {
        return true;
    }

    public void GenerateLocations()
    {
        foreach (Accommodation accommodation in Accommodations)
        {
            accommodation.Location = _locationService.Get(accommodation.LocationId);
        }

    }

    public List<String> GetAccommodationsType()
    {
        foreach (Accommodation accommodation in _accommodationService.GetAll())
        {
            if (!AccommodationsType.Contains(accommodation.Type.ToString()))
            {
                AccommodationsType.Add(accommodation.Type.ToString());
            }
        }
        AccommodationsType.Add("Any");
        return AccommodationsType;
    }

    public List<Location> GetLocationsForAccommodations()
    {
        foreach (Accommodation accommodation in _accommodationService.GetAll())
        {
            var temp = _locationService.Get(accommodation.LocationId);
            temp.ToString();
            //moram da pristupam preko id-a
        }
        return Locations;
    }

    public void LocationsToString()
    {
        foreach (Location location in Locations)
        {
            location.ToString();
        }
    }
    #endregion

    #region SearchLogic

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

        if (MaxGuestNum == 0)
        {
            MaxGuestNum = 1;
        }

        if (MinStayingDays == 0)
        {
            foreach (Accommodation accommodation in Accommodations)
            {
                MinStayingDays = accommodation.CancellationThreshold;
            }
        }

        if (SelectedType == null || SelectedType == "Any")
        {
            SelectedType = string.Empty;
        }
    }
    public void SearchLogic()
    {
        //Accommodations.Clear();
        var searchedAccommodations = new ObservableCollection<Accommodation>();
        HandleEmptyStrings();
        foreach (Accommodation accommodation in Accommodations)
        {
            if (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) && accommodation.MaxGuestNum >= MaxGuestNum
                && accommodation.MinStayingDays >= MinStayingDays &&
                accommodation.Type.ToString().Contains(SelectedType) &&
                accommodation.Location.ToString().Contains(SelectedCity + ", " + SelectedCountry))
            {
                searchedAccommodations.Add(accommodation);
            }
        }
        DataGrid.ItemsSource = searchedAccommodations;
    }
    #endregion

    private void HandleCitiesForCountry()
    {
        if (SelectedCountry != null)
        {
            Cities.Clear();
            foreach (Location location in Locations)
            {
                if (location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }
    }

    private bool CanRate()
    {
        if (SelectedAccommodation != null)
            return true;
        return false;
    }

    private bool CanCreateReservation()
    {
        if (SelectedAccommodation != null)
            return true;
        return false;
    }

    private bool CanSearch()
    {
        return true;
    }

    private void NewSearchWindow()
    {
        var newSearchWindow = new GuestOneSearchWindow();
        newSearchWindow.Show();
    }

    private void RateAccommodation()
    {
        var rateAccommodationWindow = new RateAccommodationWindow(SelectedAccommodation);
        rateAccommodationWindow.Show();
    }

    private void CreateReservation()
    {
        var createReservation = new ReservationWindow(SelectedAccommodation);
        createReservation.Show();
    }

    private void ShowReservations()
    {
        var userReservationWindow = new UserReservationsWindow();
        userReservationWindow.Show();
    }


    /*private void SelectedCountryChanged(object sender, SelectionChangedEventArgs e)
    {

        Cities.Clear();
        foreach (Location location in Locations)
        {
            if (location.Country == SelectedCountry)
            {
                Cities.Add(location.City);
            }
        }
    }*/


    /*private void SelectedCityChanged(object sender, SelectionChangedEventArgs e)
    {

        foreach (Location location in Locations)
        {
            if (location.City == SelectedCity)
            {
                SelectedCountry = location.Country;
            }
        }

    }*/
}
