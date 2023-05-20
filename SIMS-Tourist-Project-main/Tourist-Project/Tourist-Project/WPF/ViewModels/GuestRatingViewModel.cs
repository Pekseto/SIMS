using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Collections.ObjectModel;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestRatingViewModel
    {
        private GuestService _guestService = new();
        private UserService _userService = new();
        private GuestRateService _guestRateService = new();
        private AccommodationRatingService _accommodationRatingService = new();
        private User _user;

        public ObservableCollection<Reservation> ReservationsRatedOneGuest { get; set; } = new();
        public ObservableCollection<AccommodationRating> AccommodationsRatedByGuest { get; set; } = new();

        public List<GuestRating> AllGuestRatings { get; set; } = new();

        public ObservableCollection<GuestRating> FilteredGuestRatings { get; set; } = new();

        public Guest LoggedGuest { get; set; } = new();
        
        public GuestRating SelectedGuestRating { get; set; } = new();
        public GuestRatingViewModel(User user)
        {
            _user = user;
            ReservationsRatedOneGuest = GetReservationsWhichRatedGuest(_user);// rezervacije koje su ocenile mog gosta
            AccommodationsRatedByGuest = _accommodationRatingService.GetGuestGivenRatings(_user);//rezervacije koje je ocenio moj gost                                                           //treba  da nadjem rezervacije koje je ocenio moj gost i onda da mu dam ocene koje su presek skupa rezervacije koje su njega ocenile i onih koje je on ocenio
            AllGuestRatings = GetAllGuestRatings(_user);//sve ocene koje su rezervacije mom dale gostu, ali sada kao objekti GuestRating
            FilteredGuestRatings = GetFilteredGuestRatings(_user);

        }


        public ObservableCollection<Reservation> GetReservationsWhichRatedGuest(User user)//medju ovim rezervacijama treba da nadjem one koje su ocenile gosta
        {
            var ratedReservations = new ObservableCollection<Reservation>();
            ratedReservations = _guestService.GetReservationsWhichRatedGuests();//sve rezervacije koje su dale ocenu bilo kom gostu
            var guestRatedReservations = new ObservableCollection<Reservation>();
            LoggedGuest = _guestService.GetOne(_user.Id);
            guestRatedReservations = _guestService.GetReservationsWhichRatedOneGuest(LoggedGuest);//rezervacije koje su dale ocenu mom gostu
            return guestRatedReservations;//sve rezervacije koje su ocenile mog gosta
        }

        public List<GuestRating> GetAllGuestRatings(User user)
        {
            var allGuestRatings = new List<GuestRating>();
            foreach(Reservation reservation in ReservationsRatedOneGuest)
            {
                allGuestRatings.Add(_guestRateService.Get(reservation.Id));
            }
            return allGuestRatings;
        }

        public ObservableCollection<GuestRating> GetFilteredGuestRatings(User user)//u GuestRating postoji polje ReservationId i u AccommodationRating postoji ReservationId
        {
            var filteredGuestRatings = new ObservableCollection<GuestRating>();
            foreach(AccommodationRating accommodationRating in _accommodationRatingService.GetGuestGivenRatings(user))
            {
                filteredGuestRatings.Add(AllGuestRatings.Find(C => C.ReservationId == accommodationRating.ReservationId));
            }   
            return filteredGuestRatings;
        }

    }
}
