﻿using System;
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
    public class RateAccommodationViewModel
    {

        private Window _window;
        private AccommodationRatingService _accommodationRatingService = new AccommodationRatingService();
        private ReservationService _reservationService = new ReservationService();
        private AccommodationService _accommodationService = new AccommodationService();
        //sad samo treba da prihvatim parametre i da ih upisem u fajlove

        private User _user;
        public int AccommodationGrade { get; set; }

        public String Comment { get; set; }
        public String ImageUrl { get; set; }

        public String OwnerComment { get; set; }

        public int OwnerRating { get; set; }

        //treba da sacuvam AccommodationRating

        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationRating NewAccommodationRating { get; set; } = new();

        public ICommand Confirm_Command { get; set; }

        public ICommand Home_Command { get; set; }


        public RateAccommodationViewModel(Window window, User user, Accommodation selectedAccommodation)
        {
            SelectedAccommodation = selectedAccommodation;
            this._window = window;
            _user = user;
            NewAccommodationRating.UserId = _user.Id;
            Confirm_Command = new RelayCommand(RateAccommodation, CanCreate);
            Home_Command = new RelayCommand(HomeCommand, CanHome);

        }

        private bool CanHome()
        {
            return true;
        }

        private void HomeCommand()
        {
            _window.Close();
        }
        private bool CanCreate()
        {
            return true;
        }

        private void RateAccommodation()
        {
            _accommodationRatingService.Create(NewAccommodationRating);
            MessageBox.Show("You have rated this accommodation");
        }
        
    }
}
