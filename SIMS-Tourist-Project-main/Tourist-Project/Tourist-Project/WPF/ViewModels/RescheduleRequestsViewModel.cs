using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Tourist_Project.WPF.ViewModels
{
    public class RescheduleRequestsViewModel
    {
        private Window _window;

        private AccommodationService _accommodationService =  new();

        private ReservationService _reservationService =  new();

        private RescheduleRequestService rescheduleRequestService =  new();
        public Reservation SelectedReservation { get; set; }

        public RescheduleRequest SelectedRescheduleRequest { get; set; }
        public Accommodation SeekedAccommodation { get; set; } = new Accommodation();   

        public List<Reservation> ReservationsForAccommodation { get; set; } = new List<Reservation>();  
        public List<RescheduleRequest> RescheduleRequestsForReservations{ get; set; } = new List<RescheduleRequest>();

        public ObservableCollection<RescheduleRequest> PendingRequests { get; set; } = new ObservableCollection<RescheduleRequest>();
        public ObservableCollection<RescheduleRequest> AcceptedRequests { get; set; } = new ObservableCollection<RescheduleRequest>();

        public ObservableCollection<RescheduleRequest> DeclinedRequests { get; set; } = new ObservableCollection<RescheduleRequest>();

        public ICommand Home_Command { get; set; } 
        public RescheduleRequestsViewModel(Window window,Reservation selectedReservation)
        {

            Home_Command = new RelayCommand(Home, CanHome);
            this._window = window;
            SelectedReservation = selectedReservation;
            SeekedAccommodation = FindAccommodationForReservation();
            ReservationsForAccommodation = FindReservationsForAccommodation();
            RescheduleRequestsForReservations = FindRescheduleRequests();
            PendingRequests = rescheduleRequestService.GetPending();
            AcceptedRequests = rescheduleRequestService.GetAccepted();
            DeclinedRequests = rescheduleRequestService.GetDeclined();
        }

        private bool CanHome()
        {
            return true;
        }

        private void Home()
        {
            _window.Close();
        }
        public Accommodation FindAccommodationForReservation()
        {
            foreach(Accommodation accommodation in _accommodationService.GetAll())
            {
                if(accommodation.Id == SelectedReservation.Accommodation.Id)
                    SeekedAccommodation = accommodation;
            }
            return SeekedAccommodation;
        }
        //nadji sve rezervacije za zadati smestaj

        public List<Reservation> FindReservationsForAccommodation()
        {
            foreach(Reservation reservation in _reservationService.GetAll())
            {
                if(reservation.AccommodationId == SeekedAccommodation.Id)
                    ReservationsForAccommodation.Add(reservation);
            }
            return ReservationsForAccommodation;
        }

        public List<RescheduleRequest> FindRescheduleRequests()
        {
            foreach(RescheduleRequest rescheduleRequest in rescheduleRequestService.GetAll())
            {
                foreach(Reservation reservation in ReservationsForAccommodation)
                {
                    if(rescheduleRequest.ReservationId == reservation.Id)
                    {
                        RescheduleRequestsForReservations.Add(rescheduleRequest);
                    }
                }
            }
            return RescheduleRequestsForReservations;
        }

    }
}
