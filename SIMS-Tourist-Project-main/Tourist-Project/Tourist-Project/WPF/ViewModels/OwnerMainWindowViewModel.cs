using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        public static ObservableCollection<Notification> ReviewNotifications { get; set; }
        public static ObservableCollection<GuestRating> GuestRatings { get; set; }
        public static ObservableCollection<RescheduleRequest> RescheduleRequests { get; set; }
        public static ObservableCollection<AccommodationRating> AccommodationRatings { get; set; }
        private static AccommodationService accommodationService = new();
        private readonly LocationService locationService = new();
        private readonly ImageService imageService = new();
        private static NotificationService notificationService = new();
        private static ReservationService reservationService = new();
        private static GuestRateService guestRateService = new();
        private static AccommodationRatingService accommodationRatingService = new();
        private static UserService userService = new();
        private static RescheduleRequestService rescheduleRequestService = new();
        public static Accommodation SelectedAccommodation { get; set; }
        public static Notification SelectedRating { get; set; }
        public static Notification SelectedReview { get; set; }
        public static RescheduleRequest SelectedRescheduleRequest { get; set; }
        public static User user { get; set; }
        public OwnerMainWindow OwnerMainWindow { get; set; }
        public double Rating { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand RateCommand { get; set; }
        public ICommand ShowReviewsCommand { get; set; }
        public ICommand ConfirmRescheduleCommand { get; set; }
        public ICommand CancelRescheduleCommand { get; set; }
        public OwnerMainWindowViewModel(OwnerMainWindow ownerMainWindow)
        {
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            RateCommand = new RelayCommand(Rate, CanRate);
            ShowReviewsCommand = new RelayCommand(ShowReview, CanShow);
            ConfirmRescheduleCommand = new RelayCommand(ConfirmReschedule, CanConfirmReschedule);
            CancelRescheduleCommand = new RelayCommand(CancelReschedule, CanCancelReschedule);
            user = userService.GetOne(MainWindow.LoggedInUser.Id);
            OwnerMainWindow = ownerMainWindow;
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            reservations = new ObservableCollection<Reservation>(reservationService.GetAll());
            GuestRatings = new ObservableCollection<GuestRating>(guestRateService.GetAll());
            AccommodationRatings = new ObservableCollection<AccommodationRating>(accommodationRatingService.GetAll());
            RescheduleRequests = new ObservableCollection<RescheduleRequest>(rescheduleRequestService.GetByStatus(RequestStatus.Pending));
            GuestRatingNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("GuestRate"));
            ReviewNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("Reviews"));
            HasUnratedGuests();
            HasReviews();
            getRating();
            isSuper();
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

        public static void ShowReview()
        {
            var showReviewsWindow = new OwnerReviewsView();
            showReviewsWindow.ShowDialog();
        }

        public static bool CanShow()
        {
            return GuestRatingNotifications.Count == 0;
        }

        public static void ConfirmReschedule()
        {
            var reservation = reservationService.Get(SelectedRescheduleRequest.ReservationId);
            reservation.CheckIn = SelectedRescheduleRequest.NewBeginningDate;
            reservation.CheckOut = SelectedRescheduleRequest.NewEndDate;
            reservationService.Update(reservation);
            SelectedRescheduleRequest.Status = RequestStatus.Confirmed;
            rescheduleRequestService.Update(SelectedRescheduleRequest);
        }

        public static bool CanConfirmReschedule()
        {
            return SelectedRescheduleRequest != null;
        }

        public static void CancelReschedule()
        {
            var CancelRescheduleWindow = new CancelRescheduleRequest(SelectedRescheduleRequest);
            CancelRescheduleWindow.ShowDialog();
        }

        public static bool CanCancelReschedule()
        {
            return SelectedRescheduleRequest != null;
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

        public static void HasReviews()
        {
            if (AccommodationRatings.Count == 0) return;
            foreach (var accommodationRating in AccommodationRatings)
            {
                if(accommodationRating.Notified) continue;
                notificationService.Create(new Notification("Reviews", accommodationRating.UserId, accommodationRating.ReservationId));
                accommodationRating.Notified = true;
                accommodationRatingService.Update(accommodationRating);
            }
        }

        public void isSuper()
        {
            if(user.IsSuper && AccommodationRatings.Count < 10 && Rating < 4.5)
                user.IsSuper = false;
            else
                user.IsSuper = true;
            userService.Update(user);
        }

        public void showSuper()
        {
            OwnerMainWindow.Super.Visibility = user.IsSuper ? Visibility.Visible : Visibility.Hidden;
        }

        public void getRating()
        {
            Rating = (double)AccommodationRatings.Sum(accommodationRating => accommodationRating.AccommodationGrade + accommodationRating.Cleanness + accommodationRating.OwnerRating)/(AccommodationRatings.Count*3);
        }
    }

}