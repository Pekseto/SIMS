using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.WPF.Views;
using System.Windows.Input;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class UserReservationsViewModel
    {
        private Window _window;
        private UserService _userService = new();

        private ReservationService _reservationService = new(); 

        private AccommodationService _accommodationService = new();
        //public UserReservationsWindow UserReservationsWindow { get; set; }

        public List<Reservation> ReservationsForUser { get; set; } = new();

        public Reservation SelectedReservation { get; set; }

        public ICommand RescheduleReservation_Command { get; set; }
        public ICommand RateAccommodation_Command { get; set; }

        public ICommand CancelReservation_Command { get; set; }

        public ICommand ShowRescheduleRequests_Command { get; set; }

        public ICommand Home_Command { get; set; }
        public Accommodation SuitingAccommodation { get; set; } = new();
        public static User User { get; set; }   
        public UserReservationsViewModel(UserReservationsWindow userReservationsWindow, User user)
        {

            User = user;
            //UserReservationsWindow = userReservationsWindow;
            _window = userReservationsWindow;
            ReservationsForUser = GetReservationsForUser(user);
            GenerateReservations();
            RescheduleReservation_Command = new RelayCommand(RescheduleSelectedReservation, CanReschedule);
            RateAccommodation_Command = new RelayCommand(RateAccommodation, CanRateAccommodation);
            CancelReservation_Command = new RelayCommand(CancelReservation, CanCancel);
            ShowRescheduleRequests_Command = new RelayCommand(GetRescheduleRequests, CanShowRescheduleRequests);
            Home_Command = new RelayCommand(HomeCommand, CanHome);
        }

        private void GetRescheduleRequests()
        {
            var rescheduleRequests = new RescheduleRequests(SelectedReservation);
            rescheduleRequests.Show();
        }

        public bool CanRateAccommodation()
        {
            return SelectedReservation != null;
        }

        private bool CanShowRescheduleRequests()
        {
            return SelectedReservation != null;
        }

        private bool CanCancel()
        {
            return SelectedReservation != null;
        }

        public void CancelReservation()
        {
            _reservationService.Delete(SelectedReservation.Id);
            MessageBox.Show("Reservation Cancelled");
            //ReservationsForUser.Remove(SelectedReservation);
        }

        public void RateAccommodation()
        {
            Accommodation SuitngAccommodation = FindAccommodationForSelectedReservation(SelectedReservation);
            var rateAccommodationWindow = new RateAccommodationWindow(SuitngAccommodation, User);
            rateAccommodationWindow.Show();
        }

        public Accommodation FindAccommodationForSelectedReservation(Reservation selectedReservation)
        {
            foreach (Accommodation accommodation in _accommodationService.GetAll())
            {
                if(accommodation.Id == selectedReservation.Accommodation.Id)
                    return accommodation;
                else 
                {
                    return null;
                }
            }

            return null;
        }

        public void GenerateReservations()
        {
            foreach (Reservation reservation in ReservationsForUser)
            {
                reservation.Accommodation = _accommodationService.Get(reservation.Accommodation.Id);
            }
        }

        public List<Reservation> GetReservationsForUser(User loggedUser)
        {
            foreach(Reservation reservation in _reservationService.GetAll())
            {
                if(reservation.GuestId == loggedUser.Id)
                {
                    ReservationsForUser.Add(reservation);
                }
            }
            return ReservationsForUser;

        }

        public bool CanReschedule()
        {
           return SelectedReservation != null;
        }

        public void RescheduleSelectedReservation()
        {
            var rescheduleReservationWindow = new RescheduleReservation(SelectedReservation);
            rescheduleReservationWindow.Show();
         }

        private void HomeCommand()
        {
            _window.Close();
        }

        private bool CanHome()
        {
            return true;
        }
    }
}
