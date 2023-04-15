using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Windows.Input;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateReservationViewModel
    {

        private Window _window;
        private ReservationService _reservationService { get; set; }

        public List<Reservation> Reservations { get; set; }

        public String Name { get; set; }
        public int StayingDays { get; set; }
        public String Type { get; set; }

        public DateTime From { get; set; }
        public DateTime  To { get; set; }

        public List<DateTime> RequiredDates { get; set; }


        public ICommand CreateReservation_Command { get; set; }

        public CreateReservationViewModel(Window _window, Accommodation SelectedAccommodation)
        {
            this._window = _window;
            _reservationService = new ReservationService();
            Name = SelectedAccommodation.Name;
            Type = SelectedAccommodation.Type.ToString();
           // CreateReservation_Command = new RelayCommand(BookAccommodation(CanCreate);
            Reservations = _reservationService.GetAll();
            RequiredDates = new();

        }

        public bool CanCreate()
        {
            if (From != DateTime.MinValue && To != DateTime.MinValue && StayingDays != 0)
            {
                return true;
            }
            else 
                return false;
        }

        public Reservation FindReservation(Accommodation SelectedAccommodation)
        {
            foreach (Reservation reservation in Reservations)
            {
                if (reservation.AccommodationId == SelectedAccommodation.Id)
                {
                    return reservation; //rezervacija na smestaju sa tim Id-em
                }
            }
            return null;
        }

        
        private void CreateReservation(Reservation reservation)
        {
            int availableDays = 0;
            foreach(Date date in reservation.AvailableDates)
            {
                if (date.IsFree)
                {
                    availableDays++;
                    if(availableDays == StayingDays)
                    {
                        availableDays = 0;
                    }
                }
                else
                {
                    availableDays = 0;
                }
            }
        }
       
        private void BookAccommodation(Accommodation SelectedAccommodation)
        {
            Reservation reservation = FindReservation(SelectedAccommodation);
            
            CreateReservation(reservation);

            

        }


    }
}
