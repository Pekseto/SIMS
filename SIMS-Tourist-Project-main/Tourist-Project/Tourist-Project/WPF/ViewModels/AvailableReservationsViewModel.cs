using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.WPF.Views;
using Tourist_Project.Domain.Models;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Tourist_Project.WPF.ViewModels
{
    public class AvailableReservationsViewModel
    {

        public struct DateSpan
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public List<DateSpan> AvailableDates { get; set; } = new();


        private Window _window;
        private User _user;
        private ReservationService _reservationService;
        private GuestService _guestService;
        public List<Reservation> ReservationsForAccommodation { get; set; }
        
        public List<DateTime> FreeDates { get; set; }

        public DateTime SearchedStartingDate { get; set; }
        public DateTime SearchedEndingDate { get; set; }

        public Accommodation SelectedAccommodation { get; set; } = new();

        public int StayingDays { get; set; }    

        public ICommand ConfirmReservation_Command { get; set; }
        public ICommand Cancel_Command { get; set; }

        public String Name { get; set; }
        public Reservation SelectedReservation { get; set; } = new();

        public ObservableCollection<Reservation> ReservationsForDisplay { get; set; }

        
        public int GuestsNum { get; set; }  
        public List<String> StartingDates { get; set; }
        public List<String> EndingDates { get; set; }   
        public AvailableReservationsViewModel(Window window, Accommodation selectedAccommodation, DateTime from, DateTime to, int stayingDays, int guestsNum, User user)
        {
            _window = window;
            _user = user;
            _reservationService = new ReservationService();
            _guestService = new GuestService();
            ReservationsForAccommodation = new List<Reservation>();
            ReservationsForDisplay = new ObservableCollection<Reservation>();
            FreeDates = new List<DateTime>();
            ReservationsForAccommodation = _reservationService.FindReservationsForAccommodation(selectedAccommodation);
            SearchedStartingDate = from;
            SearchedEndingDate = to;
            StayingDays = stayingDays;
            SelectedReservation = new Reservation();
            Name = selectedAccommodation.Name;
            SelectedAccommodation = selectedAccommodation;
            
            GuestsNum = guestsNum;
            CheckFreeDays();
            GenerateRelevantReservations();
            ConfirmReservation_Command = new RelayCommand(SaveReservation, CanReserve);
            Cancel_Command = new RelayCommand(CloseWindow, CanClose);
        }

        private void CheckFreeDays()
        {
            double timeSpan = 0;
            while(AvailableDates.Count == 0)
            {
                timeSpan = SearchedEndingDate.Subtract(SearchedStartingDate).Days;
                UpdateAvailableDates(SearchedStartingDate.AddDays(timeSpan), SearchedEndingDate.AddDays(timeSpan));
            }
        }


        private void GenerateRelevantReservations()
        {
            
            foreach (DateSpan ds in AvailableDates)
            {
                Reservation reservation = new Reservation(ds.Start, ds.End, GuestsNum, StayingDays, SelectedAccommodation);
                ReservationsForDisplay.Add(reservation);
            }
            
        }

        private void UpdateAvailableDates(DateTime searchedStartingDate, DateTime searchedEndingDate)
        {
            DateTime possibleStartingDate = searchedStartingDate;
            DateTime possibleEndingDate = searchedStartingDate.AddDays(StayingDays);

            while (searchedEndingDate.Date >= possibleEndingDate.Date) 
            {

                bool isDateConflicted = ConflictionExists(possibleStartingDate, possibleEndingDate);

                AddFreeDatesToList(possibleStartingDate, possibleEndingDate, isDateConflicted);

                possibleStartingDate = possibleEndingDate.AddDays(1);
                possibleEndingDate = possibleStartingDate.AddDays(StayingDays);

            }

        }

        private void AddAvailableDates(bool areDatesInConflicts, DateTime possibleStartingDate, DateTime possibleEndingDate)
        {
            if (areDatesInConflicts)
            {
                Reservation reservation = new Reservation(possibleStartingDate, possibleEndingDate, StayingDays, GuestsNum, SelectedAccommodation);
                DateTime dt = possibleStartingDate;
                while(dt <= possibleEndingDate)
                {
                    dt.AddDays(1);
                    FreeDates.Add(dt);
                    if (FreeDates.Count > 30)
                        break;
                }
                
                ReservationsForDisplay.Add(reservation);
            }
        }
        private bool ConflictionExists(DateTime possibleStartingDate, DateTime possibleEndingDate)
        {
            foreach (var reservation in ReservationsForAccommodation)
            {
                bool areDatesConflicted = reservation.CheckIn.Date <= possibleEndingDate.Date && possibleStartingDate.Date <= reservation.CheckOut.Date;

                if (areDatesConflicted)
                    return true;
            }
            return false;
        }

        private void AddFreeDatesToList(DateTime possibleStartingDate, DateTime possibleEndingDate, bool isDateConflicted)
        {
            if (isDateConflicted) return;

            var tempDateSpan = new DateSpan();
            tempDateSpan.Start = possibleStartingDate;
            tempDateSpan.End = possibleEndingDate;
            AvailableDates.Add(tempDateSpan);

        }

        private bool CanReserve()
        {
            return SelectedReservation != null;
        }

        private void SaveReservation()
        {
            SelectedReservation.GuestId = _user.Id;
            _reservationService.Create(SelectedReservation);
            HandleSuperGuest();
            ReservationsForDisplay.Remove(SelectedReservation);
            MessageBox.Show("Your reservation has been confirmed");
        }

        private void HandleSuperGuest()
        {
            var guestId = _user.Id;
            var guest = _guestService.GetOne(guestId);
                if(_guestService.GetRelevantReservations(guest).Count() >= 10)
                {
                    _guestService.HandleSuperGuest(guest);
                    _guestService.DecrementPoints(guest);
                    _guestService.Update(guest);
                }
                else
                {
                    guest.IsSuper = false;
                    guest.Points = 0;
                    _guestService.Update(guest);
                }
            
        }

        private bool CanClose()
        {
            return true;
        }

        private void CloseWindow()
        {
            _window.Close();
        }
    }
}
