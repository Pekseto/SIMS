using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class OwnerMainWindowViewModel 
    {
        #region Collections
        private ObservableCollection<Notification> guestRatingNotifications;
        public ObservableCollection<Notification> GuestRatingNotifications
        {
            get => guestRatingNotifications;
            set
            {
                if (value == guestRatingNotifications) return;
                guestRatingNotifications = value;
                OnPropertyChanged(nameof(GuestRatingNotifications));
            }
        }
        private ObservableCollection<Notification> reviewNotifications;
        public ObservableCollection<Notification> ReviewNotifications
        {
            get => reviewNotifications;
            set
            {
                if(value == reviewNotifications) return;
                reviewNotifications = value;
                OnPropertyChanged(nameof(ReviewNotifications));
            }
        }
        private ObservableCollection<ReschedulingReservationViewModel> rescheduleRequests;
        public ObservableCollection<ReschedulingReservationViewModel> RescheduleRequests
        {
            get => rescheduleRequests;
            set
            {
                if(value == rescheduleRequests) return;
                rescheduleRequests = value;
                OnPropertyChanged(nameof(RescheduleRequests));
            }
        }
        private ObservableCollection<AccommodationRating> accommodationRatings;
        public ObservableCollection<AccommodationRating> AccommodationRatings
        {
            get => accommodationRatings;
            set
            {
                if(value == accommodationRatings) return;
                accommodationRatings = value;
                OnPropertyChanged(nameof(AccommodationRatings));
            }
        }
        private ObservableCollection<AccommodationViewModel> accommodationView;
        public ObservableCollection<AccommodationViewModel> AccommodationView
        {
            get => accommodationView;
            set
            {
                if(value == accommodationView) return;
                accommodationView = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Services
        private static AccommodationService accommodationService = new();
        private static NotificationService notificationService = new();
        private static ReservationService reservationService = new();
        private static AccommodationRatingService accommodationRatingService = new();
        private static UserService userService = new();
        private static RescheduleRequestService rescheduleRequestService = new();
        #endregion
        #region SelectedProperties
        public static AccommodationViewModel SelectedAccommodation { get; set; }
        public static Notification SelectedRating { get; set; }
        public static ReschedulingReservationViewModel SelectedRescheduleRequest { get; set; } 
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
        public ICommand LogOutCommand { get; set; }
        #endregion
        public OwnerMainWindowViewModel(OwnerMainWindow ownerMainWindow, User user)
        {
            OwnerMainWindow = ownerMainWindow;
            notificationService.HasUnratedGuests();
            notificationService.HasReviews();
            #region CommandInstanting
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            RateCommand = new RelayCommand(Rate, CanRate);
            ShowReviewsCommand = new RelayCommand(ShowReview, CanShow);
            LogOutCommand = new RelayCommand(LogOut);
            #endregion
            #region CollectionInstanting
            User = userService.Get(user.Id);
            AccommodationRatings = new ObservableCollection<AccommodationRating>(accommodationRatingService.GetAll());
            RescheduleRequests = new ObservableCollection<ReschedulingReservationViewModel>(rescheduleRequestService.GetAll().Where(rescheduleRequest => rescheduleRequest.Status == RequestStatus.Pending).Select(rescheduleRequest => new ReschedulingReservationViewModel(rescheduleRequest)));
            GuestRatingNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("GuestRate"));
            ReviewNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("Reviews").Where(notification => notification.Notified == false));
            AccommodationView = new ObservableCollection<AccommodationViewModel>(accommodationService.GetAll().Select(accommodation => new AccommodationViewModel(accommodation)));
            #endregion
            Rating = accommodationRatingService.getRating();
            showSuper();
        }
        #region CommandsLogic
        public void Create()
        {
            var createWindow = new CreateAccommodation(User);
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
            return SelectedAccommodation != null;
        }

        public static void Delete()
        {
            accommodationService.Delete(SelectedAccommodation.Accommodation.Id);
        }
        public static bool CanDelete()
        {
            return SelectedAccommodation != null;
        }

        public void Rate()
        {
            var rateWindow = new RateGuestWindow(SelectedRating, this);
            rateWindow.ShowDialog();
        }
        public static bool CanRate()
        {
            return SelectedRating != null;
        }

        public void ShowReview()
        {
            var showReviewsWindow = new OwnerReviewsView();
            foreach (var notification in ReviewNotifications)
            {
                notification.Notified = true;
                notificationService.Update(notification);
            }
            ReviewNotifications.Clear();
            ReviewNotifications = new ObservableCollection<Notification>(notificationService.GetAllByType("GuestRate"));
            showReviewsWindow.ShowDialog();
        }
        public bool CanShow()
        {
            return GuestRatingNotifications.Count == 0;
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

        public void GuestRateUpdate(Notification notification)
        {
            GuestRatingNotifications.Remove(notification);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}