using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class ConfirmPresenceViewModel : ViewModelBase
    {
        private readonly NotificationGuestTwoService notificationService = new();
        private readonly TourAttendanceService attendanceService = new();
        private readonly TourService tourService = new();
        public string Text { get; set; }
        public Tour Tour { get; set; }
        public NotificationGuestTwo Notification { get; set; }
        public TourAttendance TourAttendance { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }

        public ConfirmPresenceViewModel(NotificationGuestTwo notification)
        {
            Notification = notification;

            TourAttendance = attendanceService.GetByTourIdAndUserId(notification.TourId, notification.UserId);
            Tour = tourService.GetOne(notification.TourId);

            Text = "The tour guide has called you out for the tour " + Tour.Name + ". Please confirm if you are present by clicking on the Yes button, or No otherwise!";


            YesCommand = new RelayCommand(OnYesClick, () => !Notification.Responded);
            NoCommand = new RelayCommand(OnNoClick, () => !Notification.Responded);
        }

        private void OnNoClick()
        {
            TourAttendance.Presence = Presence.No;
            Notification.Responded = true;

            attendanceService.Update(TourAttendance);
            notificationService.Update(Notification);
        }

        private void OnYesClick()
        {
            TourAttendance.Presence = Presence.Yes;
            Notification.Responded = true;

            attendanceService.Update(TourAttendance);
            notificationService.Update(Notification);
        }
    }
}
