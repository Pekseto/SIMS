using System;
using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.View;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<GuestReview> GuestReviews { get; set; } = new();
        public static ObservableCollection<Reservation> Reservations { get; set; } = new();
        public static User LoggedInUser { get; set; }
        private readonly GuestReviewRepository guestReviewRepository = new();
        private readonly ReservationService reservationService = new();
        public static GuestReview? UnreviewedGuest { get; set; }
        public MainWindow(User user)
        {
            DataContext = this;
            InitializeComponent();
            LoggedInUser = user;
            GuestReviews = new ObservableCollection<GuestReview>(guestReviewRepository.GetAll());
            Reservations = new ObservableCollection<Reservation>(reservationService.GetAll());
        }

        private void OwnerButtonClick(object sender, RoutedEventArgs e)
        {
            if (HasUnreviewedGuests())
            {
                MessageBoxResult result = MessageBox.Show("You have unreviewed guests. Do you want to grade them?", "Grade guest",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var guestRevisionWindow = new GuestRevision(UnreviewedGuest.GuestId, UnreviewedGuest.OwnerId, UnreviewedGuest);
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
                    var daysSinceCheckOut = DateTime.Now - reservation.CheckOut;
                    if (guestReview.IsReviewed() || daysSinceCheckOut.Days >= 5 ||
                        guestReview.GuestId != reservation.GuestId) continue;
                    UnreviewedGuest = guestReview;
                    return true;
                }
            }
            return false;
        }

        private void Guest1ButtonClick(object sender, RoutedEventArgs e)
        {
            var guestOne = new GuestOne(LoggedInUser);
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
            var guideShowWindow = new TodayToursView();
            guideShowWindow.Show();
        }
    }
}
