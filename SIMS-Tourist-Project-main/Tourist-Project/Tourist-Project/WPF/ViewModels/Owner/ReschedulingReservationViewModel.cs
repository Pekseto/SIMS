using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class ReschedulingReservationViewModel : INotifyPropertyChanged
    {
        private RescheduleRequest rescheduleRequest;

        public RescheduleRequest RescheduleRequest
        {
            get => rescheduleRequest;
            set
            {
                if (value == rescheduleRequest) return;
                rescheduleRequest = value;
                OnPropertyChanged("RescheduleRequest");
            }
        }

        private Reservation reservation;
        public Reservation Reservation
        {
            get => reservation;
            set
            {
                if(value == reservation) return;
                reservation = value;
                OnPropertyChanged("Reservation");
            }
        }

        private Accommodation accommodation;
        public Accommodation Accommodation
        {
            get => accommodation;
            set
            {
                if(value == accommodation) return;
                accommodation = value;
                OnPropertyChanged("Accommodation");
            }
        }

        private User user;
        public User User
        {
            get => user;
            set
            {
                if(value == user) return;
                user = value;
                OnPropertyChanged("User");
            }
        }

        private readonly ReservationService reservationService = new();
        private readonly AccommodationService accommodationService = new();
        private readonly UserService userService = new();
        private readonly RescheduleRequestService rescheduleRequestService = new();

        public ICommand ConfirmRescheduleCommand { get; set; }
        public ICommand CancelRescheduleCommand { get; set; }
        public ReschedulingReservationViewModel(RescheduleRequest rescheduleRequest)
        {
            RescheduleRequest = rescheduleRequest;
            Reservation = reservationService.Get(RescheduleRequest.ReservationId);
            Accommodation = accommodationService.Get(Reservation.AccommodationId);
            User = userService.Get(Reservation.GuestId);

            ConfirmRescheduleCommand = new RelayCommand(ConfirmReschedule, CanConfirmReschedule);
            CancelRescheduleCommand = new RelayCommand(CancelReschedule, CanCancelReschedule);
        }

        public void ConfirmReschedule()
        {
            reservation.CheckIn = RescheduleRequest.NewBeginningDate;
            reservation.CheckOut = RescheduleRequest.NewEndDate;
            reservationService.Update(reservation);
            RescheduleRequest.Status = RequestStatus.Confirmed;
            rescheduleRequestService.Update(RescheduleRequest);
        }
        public static bool CanConfirmReschedule()
        {
            return true;
        }

        public void CancelReschedule()
        {
            var cancelRescheduleWindow = new CancelRescheduleRequest(RescheduleRequest);
            cancelRescheduleWindow.ShowDialog();
        }
        public static bool CanCancelReschedule()
        {
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}