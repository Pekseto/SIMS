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
        public UserReservationsWindow UserReservationsWindow { get; set; }

        public List<Reservation> ReservationsForUser { get; set; } = new();

        public Reservation SelectedReservation { get; set; }

        public ICommand RescheduleReservation_Command { get; set; }
        public ICommand RateAccommodation_Command { get; set; }

        public ICommand CancelReservation_Command { get; set; }

        public ICommand ShowRescheduleRequests_Command { get; set; }
        public Accommodation SuitingAccommodation { get; set; } = new();
        public static User user { get; set; }   
        public UserReservationsViewModel(UserReservationsWindow userReservationsWindow)
        {
            
            user = _userService.GetOne(MainWindow.LoggedInUser.Id);
            UserReservationsWindow = userReservationsWindow;
            ReservationsForUser = GetReservationsForUser(user);
            GenerateReservations();
            RescheduleReservation_Command = new RelayCommand(RescheduleSelectedReservation, CanReschedule);
            RateAccommodation_Command = new RelayCommand(RateAccommodation, CanRateAccommodation);
            CancelReservation_Command = new RelayCommand(CancelReservation, CanCancel);
            ShowRescheduleRequests_Command = new RelayCommand(GetRescheduleRequests, CanShowRescheduleRequests);
        }

        private void GetRescheduleRequests()
        {
            var rescheduleRequests = new RescheduleRequests(SelectedReservation);
            rescheduleRequests.Show();
        }

        public bool CanRateAccommodation()
        {
            if (SelectedReservation != null)
                return true;
            else
                return false;
        }

        public bool CanShowRescheduleRequests()
        {
            if(SelectedReservation != null)
                return true;
            else 
                return false ;
        }

        public bool CanCancel()
        {
            
            //SuitingAccommodation = FindAccommodationForSelectedReservation(SelectedReservation);
            //int dateDifference = (SelectedReservation.CheckIn - DateTime.Now).Days;
            if (SelectedReservation != null/* && dateDifference >= SuitingAccommodation.CancellationThreshold*/)
            {
                return true;
            }
            else
                return false;
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
            var rateAccommodationWindow = new RateAccommodationWindow(SuitngAccommodation);
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
            if(SelectedReservation != null)
            {
                return true;
            }
            else
                return false;
        }

        public void RescheduleSelectedReservation()
        {
            var rescheduleReservationWindow = new RescheduleReservation(SelectedReservation);
            rescheduleReservationWindow.Show();
         }

    }
}
