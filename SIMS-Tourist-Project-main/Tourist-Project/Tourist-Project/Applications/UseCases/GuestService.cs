using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Collections.ObjectModel;

namespace Tourist_Project.Applications.UseCases
{
    public class GuestService
    {
        private static readonly Injector injector = new();
        private readonly IGuestRepository guestRepository = injector.CreateInstance<IGuestRepository>();
        private GuestRateService guestRateService = new();
        private ReservationService reservationService = new();

        public GuestService()
        {
        }

        public Guest Create(Guest guest)
        {
           return guestRepository.Save(guest);
        }

        public List<Guest> GetAll()
        {
            return guestRepository.GetAll();
        }

        public Guest GetOne(int id)
        {
            return guestRepository.GetOne(id);  
        }

        public Guest Update(Guest guest)
        {
            return guestRepository.Update(guest);
        }

        public void Delete(int id)
        {
            guestRepository.Delete(id);
        }

        public ObservableCollection<Reservation> GetReservationsWhichRatedGuests()//sve "rezervacije" koje su ocenile bilo kog gosta
        {
            var accommodationsWhichGaveRating = new ObservableCollection<Reservation>();
            var reservations = reservationService.GetAll();
            foreach(GuestRating guestRating in guestRateService.GetAll())
            {
                accommodationsWhichGaveRating.Add(reservations.Find(C => C.Id == guestRating.ReservationId));
            }
            return accommodationsWhichGaveRating;
        }

        public ObservableCollection<Reservation> GetReservationsWhichRatedOneGuest(Guest guest)//sve rezervacije koje su ocenile mog gosta
        {
            var guestRatedReservations = new ObservableCollection<Reservation>();
            foreach(Reservation reservation in GetReservationsWhichRatedGuests())
            {
                if(reservation.GuestId == guest.GuestId)
                {
                    guestRatedReservations.Add(reservation);
                }
            }
            return guestRatedReservations;
        }

        public ObservableCollection<Reservation> GetGuestMadeReservations(Guest guest)
        {
            ObservableCollection<Reservation> guestMadeReservations = new ObservableCollection<Reservation>();
            foreach(Reservation reservation in reservationService.GetAll())
            {
                if(reservation.GuestId == guest.GuestId)
                {
                    guestMadeReservations.Add(reservation);
                }
            }
            return guestMadeReservations;
        }


        public ObservableCollection<Reservation> GetRelevantReservations(Guest guest)
        {
            ObservableCollection<Reservation> guestMadeReservations = GetGuestMadeReservations(guest);
            ObservableCollection<Reservation> relevantReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in guestMadeReservations)
            {
                if (reservation.CheckIn.AddYears(1) > DateTime.Now)
                {
                    relevantReservations.Add(reservation);
                }
            }
            return relevantReservations;
        }

        public Guest HandleSuperGuest(Guest guest)
        {
            ObservableCollection<Reservation> relevantReservations = GetRelevantReservations(guest);
            Guest handlingGuest = guest;
            if(relevantReservations.Count() >= 10 && !guest.IsSuper)
            {
                handlingGuest.IsSuper = true;
                handlingGuest.Points = 5;
                handlingGuest.SuperGuestBeginnig = relevantReservations.Min(x => x.CheckIn);
                handlingGuest.SuperGuestEnding = guest.SuperGuestBeginnig.AddYears(1);
            }
            return handlingGuest;
        }

        public Guest DecrementPoints(Guest guest)
        {
            var handlingGuest = guest;
            if(handlingGuest.Points <= 5 && guest.Points > 0)
            {
                handlingGuest.Points--;
            }
            return handlingGuest;
        }
    } 
}
