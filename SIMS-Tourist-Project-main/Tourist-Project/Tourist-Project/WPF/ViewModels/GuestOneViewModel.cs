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
    public String Username { get; set; }
    public AccommodationViewModel SelectedAccommodationViewModel { get; set; }

    public Accommodation SelectedAccommodation { get; set; }
    public String AccommodationName { get; set; }
    #endregion

    #region Presentation

    public ObservableCollection<AccommodationViewModel> AccommodationsViewModel { get; set; } = new();


    #endregion
    private Window _window;

    public ICommand RateAccommodation_Command { get; set; }
    public ICommand CreateReservation_Command { get; set; }
    public ICommand UserReservations_Command { get; set; }
    public ICommand Search_Command { get; set; }
    public ICommand ShowAll_Command { get; set; }
    public ICommand ViewAccommodation_Command { get; set; }
    public ICommand UserGraph_Command { get; set; }

    public ICommand ShowUserRatings_Command { get; set; }

    public DataGrid GuestOneDataGrid;

    public User User { get; set; }
    public GuestOneViewModel(Window window, DataGrid dataGrid, User user)
    {
        this._window = window;
        this.GuestOneDataGrid = dataGrid;

        AccommodationsViewModel = new ObservableCollection<AccommodationViewModel>(_accommodationService.GetAll().Select(accommodation => new AccommodationViewModel(accommodation)));
        User = user;
        Username = user.Username;
        RateAccommodation_Command = new RelayCommand(RateAccommodation, CanRate);
        CreateReservation_Command = new RelayCommand(CreateReservation, CanCreateReservation);
        UserReservations_Command = new RelayCommand(ShowReservations, CanShowReservations);
        Search_Command = new RelayCommand(ShowSearchWindow, CanSearch);
        ShowAll_Command = new RelayCommand(ShowAll, CanShowAll);
        ViewAccommodation_Command = new RelayCommand(ViewAccommodation, CanView);
        UserGraph_Command = new RelayCommand(ShowUserGraph, CanShowUserGraph);
        ShowUserRatings_Command = new RelayCommand(ShowRatings, CanShowRatings);
    }

    #region Search
    private bool CanSearch()
    {
        return true;
    }
    private void ShowSearchWindow()
    {
        var searchWindow = new GuestOneSearchWindow(GuestOneDataGrid, AccommodationsViewModel);
        searchWindow.Show();
    }
    #endregion

    #region ShowReservations
    private bool CanShowReservations()
    {
        return true;
    }
    private void ShowReservations()
    {
        var userReservationWindow = new UserReservationsWindow(User);
        userReservationWindow.Show();
    }
    #endregion

    #region RateAccommodation
    private bool CanRate()
    {
        if (SelectedAccommodationViewModel == null)
            return true;
        else
            return false;
    }
    private void RateAccommodation()
    {
        var rateAccommodationWindow = new RateAccommodationWindow(SelectedAccommodation, User);
        rateAccommodationWindow.Show();
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
    }
    #endregion
    #region CreateReservation
    private bool CanCreateReservation()
    {
        return SelectedAccommodationViewModel != null;
    }
    private void CreateReservation()
    {
        SelectedAccommodation = SelectedAccommodationViewModel.Accommodation;
        var createReservation = new ReservationWindow(SelectedAccommodation, User);
        createReservation.Show();
    }
    #endregion
    #region ViewAccommodation
    private bool CanView()
    {
        return SelectedAccommodationViewModel != null;
    }
    private void ViewAccommodation()
    {

    }
    #endregion
    #region UserGraph
    private bool CanShowUserGraph()
    {
        return true;
    }

    private void ShowUserGraph()
    {
        
    }
    #endregion

    #region Show User Reservations
    private bool CanShowRatings()
    {
        return true;
    }

    private void ShowRatings()
    {
        var userRatings = new GuestRatingView(User);
        userRatings.Show();
    }
        #endregion
    #region Shortcuts

    #endregion
}
