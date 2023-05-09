using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;

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

        public RescheduleRequestsViewModel(Window window,Reservation selectedReservation)
        {
            this._window = window;
            SelectedReservation = selectedReservation;
            SeekedAccommodation = FindAccommodationForReservation();
            ReservationsForAccommodation = FindReservationsForAccommodation();
            RescheduleRequestsForReservations = FindRescheduleRequests();

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
                if(reservation.Accommodation.Id == SeekedAccommodation.Id)
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
