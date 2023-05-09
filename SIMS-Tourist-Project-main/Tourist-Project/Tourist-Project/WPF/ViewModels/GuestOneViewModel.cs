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
    public AccommodationViewModel SelectedAccommodation { get; set; }

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

    private DataGrid _dataGrid;
    public GuestOneViewModel(Window window,DataGrid dataGrid)
    {
        this._window = window;
        this._dataGrid = dataGrid;
        
        AccommodationsViewModel = new ObservableCollection<AccommodationViewModel>(_accommodationService.GetAll().Select(accommodation => new AccommodationViewModel(accommodation)));
        RateAccommodation_Command = new RelayCommand(RateAccommodation, CanRate);
        CreateReservation_Command = new RelayCommand(CreateReservation, CanCreateReservation);
        UserReservations_Command = new RelayCommand(ShowReservations, CanShowReservations);
        Search_Command = new RelayCommand(ShowSearchWindow, CanSearch);
       
    }

    #region Search
    private bool CanSearch()
    {
        return true;
    }
    private void ShowSearchWindow()
    {
        var searchWindow = new GuestOneSearchWindow();
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
        var userReservationWindow = new UserReservationsWindow();
        userReservationWindow.Show();
    }
    #endregion

    #region RateAccommodation
    private bool CanRate()
    {
        return !(SelectedAccommodation == null);
    }
    private void RateAccommodation()
    {
        var rateAccommodationWindow = new RateAccommodationWindow(SelectedAccommodation);
        rateAccommodationWindow.Show();
    }
    #endregion



    #region CreateReservation
    private bool CanCreateReservation()
    {
        if (SelectedAccommodation != null)
            return true;
        return false;
    }
    private void CreateReservation()
    {
        var createReservation = new ReservationWindow(SelectedAccommodation);
        createReservation.Show();
    }
    #endregion


}
