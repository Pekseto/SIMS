﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<GuestReview> GuestReviews { get; set; } = new();
        public static ObservableCollection<Reservation> Reservations { get; set; } = new();
        public User LoggedInUser { get; set; }
        private readonly GuestReviewRepository guestReviewRepository = new();
        private readonly ReservationRepository reservationRepository = new();
        public static GuestReview? UnreviewedGuest { get; set; }
        public MainWindow(User user)
        {
            DataContext = this;
            InitializeComponent();
            LoggedInUser = user;
            GuestReviews = new ObservableCollection<GuestReview>(guestReviewRepository.GetAll());
            Reservations = new ObservableCollection<Reservation>(reservationRepository.GetAll());
        }

        private void OwnerButtonClick(object sender, RoutedEventArgs e)
        {
            if (HasUnreviewedGuests())
            {
                MessageBoxResult result = MessageBox.Show("You have unreviewed guests. Do you want to grade them?", "Grade guest",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var guestRevisionWindow = new GuestRevision(UnreviewedGuest.guestId, UnreviewedGuest.ownerId, UnreviewedGuest);
                    guestRevisionWindow.Show();
                }
                else
                {
                    var ownerShowWindow = new OwnerShowWindow();
                    ownerShowWindow.Show();
                }
            }
            else
            {
                var ownerShowWindow = new OwnerShowWindow();
                ownerShowWindow.Show();
            }
        }

        public static bool HasUnreviewedGuests()
        {
            foreach (var guestReview in GuestReviews)
            {
                foreach (var reservation in Reservations)
                {
                    TimeSpan daysSinceCheckOut = DateTime.Now - reservation.CheckOut;
                    if (!guestReview.IsReviewed() && daysSinceCheckOut.Days < 5 && guestReview.guestId == reservation.GuestId)
                    {
                        UnreviewedGuest = guestReview;
                        return true;
                    }
                }
            }
            return false;
        }

        private void Guest1ButtonClick(object sender, RoutedEventArgs e)
        {
            var guestOne = new GuestOne();
            guestOne.Show();
        }
        private void Guest2ButtonClick(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser.Role == UserRole.guest)
            {
                var guestTwoWindow = new GuestTwoWindow(LoggedInUser);
                guestTwoWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong user role");
            }
        }
        private void GuideButtonClick(object sender, RoutedEventArgs e)
        {
            var guideShowWindow = new GuideShowWindow();
            guideShowWindow.Show();
        }
    }
}