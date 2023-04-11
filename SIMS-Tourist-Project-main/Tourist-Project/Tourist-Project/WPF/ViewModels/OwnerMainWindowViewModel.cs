using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class OwnerMainWindowViewModel
    {
        public static ObservableCollection<Accommodation> accommodations { get; set; }
        public static ObservableCollection<Reservation> reservations { get; set; }
        public static ObservableCollection<Notification> GuestRatingNotifications { get; set; }
        public static ObservableCollection<GuestRating> GuestRatings { get; set; }
        private static AccommodationService accommodationService = new();
        private readonly LocationService locationService = new();
        private readonly ImageService imageService = new();
        private static NotificationService notificationService = new();
        private static ReservationService reservationService = new();
        private static GuestRateService guestRateService = new();
        public static Accommodation SelectedAccommodation { get; set; }
        public static Notification SelectedRating { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand RateCommand { get; set; }
        public OwnerMainWindowViewModel()
        {
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            RateCommand = new RelayCommand(Rate, CanRate);
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            reservations = new ObservableCollection<Reservation>(reservationService.GetAll());
            GuestRatings = new ObservableCollection<GuestRating>(guestRateService.GetAll());
            GuestRatingNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("GuestRate"));
            HasUnratedGuests();
            foreach (var accommodation in accommodations)
            {
                accommodation.Location = locationService.Get(accommodation.LocationId);
                accommodation.ImageUrl = imageService.Get(accommodation.ImageId).Url;
            }
        }

        public static void Create()
        {
            var createWindow = new CreateAccommodation();
            createWindow.ShowDialog();
        }

        public static bool CanCreate()
        {
            return true;
        }

        public static void Update()
        {
            var updateWindow = new UpdateAccommodation(SelectedAccommodation);
            updateWindow.ShowDialog();
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
        }

        public static bool CanUpdate()
        {
            return true;
        }
        public static void Rate()
        {
            var rateWindow = new RateGuestWindow(SelectedRating);
            rateWindow.ShowDialog();
        }

        public static bool CanRate()
        {
            return true;
        }
        public static void HasUnratedGuests()
        {
            foreach (var guestRate in GuestRatings)
            {
                foreach (var reservation in reservations)
                {
                    var daysSinceCheckOut = DateTime.Now - reservation.CheckOut;
                    if (guestRate.IsReviewed() || daysSinceCheckOut.Days >= 5 ||
                        guestRate.GuestId != reservation.GuestId) continue;
                    if(GuestRatingNotifications.Count == 0)
                            notificationService.Create(new Notification("GuestRate", guestRate.Id, reservation.Id));
                    foreach (var notification in GuestRatingNotifications)
                    {
                        if(notification.GuestRatingId != guestRate.Id && notification.ReservationId != reservation.Id)
                            notificationService.Create(new Notification("GuestRate", guestRate.Id, reservation.Id));
                    }
                }
            }
        }
    }

}