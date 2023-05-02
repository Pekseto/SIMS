using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class OwnerMainWindowViewModel
    {
        #region Collections
        public static ObservableCollection<Reservation> reservations { get; set; }
        public static ObservableCollection<Notification> GuestRatingNotifications { get; set; }
        public static ObservableCollection<Notification> ReviewNotifications { get; set; }
        public static ObservableCollection<GuestRating> GuestRatings { get; set; }
        public static ObservableCollection<RescheduleRequest> RescheduleRequests { get; set; }
        public static ObservableCollection<AccommodationRating> AccommodationRatings { get; set; }
        public static ObservableCollection<AccommodationViewModel> AccommodationView { get; set; }
        #endregion
        #region Services
        private static AccommodationService accommodationService = new();
        private static NotificationService notificationService = new();
        private static ReservationService reservationService = new();
        private static GuestRateService guestRateService = new();
        private static AccommodationRatingService accommodationRatingService = new();
        private static UserService userService = new();
        private static RescheduleRequestService rescheduleRequestService = new();
        #endregion
        #region SelectedProperties
        public static AccommodationViewModel SelectedAccommodation { get; set; }
        public static Notification SelectedRating { get; set; }
        public static RescheduleRequest SelectedRescheduleRequest { get; set; } 
        #endregion
        public static User User { get; set; }
        public OwnerMainWindow OwnerMainWindow { get; set; }
        public double Rating { get; set; }
        #region Commands
        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RateCommand { get; set; }
        public ICommand ShowReviewsCommand { get; set; }
        public ICommand ConfirmRescheduleCommand { get; set; }
        public ICommand CancelRescheduleCommand { get; set; } 
        public ICommand LogOutCommand { get; set; }
        #endregion
        public OwnerMainWindowViewModel(OwnerMainWindow ownerMainWindow, User user)
        {
            OwnerMainWindow = ownerMainWindow;
            #region CommandInstanting
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            RateCommand = new RelayCommand(Rate, CanRate);
            ShowReviewsCommand = new RelayCommand(ShowReview, CanShow);
            ConfirmRescheduleCommand = new RelayCommand(ConfirmReschedule, CanConfirmReschedule);
            CancelRescheduleCommand = new RelayCommand(CancelReschedule, CanCancelReschedule);
            LogOutCommand = new RelayCommand(LogOut);
            #endregion
            #region CollectionInstanting
            User = userService.GetOne(user.Id);
            reservations = new ObservableCollection<Reservation>(reservationService.GetAll());
            GuestRatings = new ObservableCollection<GuestRating>(guestRateService.GetAll());
            AccommodationRatings = new ObservableCollection<AccommodationRating>(accommodationRatingService.GetAll());
            RescheduleRequests = new ObservableCollection<RescheduleRequest>(rescheduleRequestService.GetByStatus(RequestStatus.Pending));
            GuestRatingNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("GuestRate"));
            ReviewNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("Reviews"));
            AccommodationView = new ObservableCollection<AccommodationViewModel>(accommodationService.GetAll().Select(accommodation => new AccommodationViewModel(accommodation)));
            #endregion
            notificationService.HasUnratedGuests();
            notificationService.HasReviews();
            Rating = accommodationRatingService.getRating();
            showSuper();
        }
        #region CommandsLogic
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
            var updateWindow = new UpdateAccommodation(SelectedAccommodation.Accommodation);
            updateWindow.ShowDialog();
        }
        public static bool CanUpdate()
        {
            return true;
        }

        public static void Delete()
        {
            accommodationService.Delete(SelectedAccommodation.Accommodation.Id);
        }
        public static bool CanDelete()
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

        public void LogOut()
        {
            var loginWindow = new LoginWindow();
            OwnerMainWindow.Close();
            loginWindow.ShowDialog();
        }
        #endregion
        
        public void showSuper()
        {
            OwnerMainWindow.Super.Visibility = userService.IsSuper(User) ? Visibility.Visible : Visibility.Hidden;
        }
    }

}