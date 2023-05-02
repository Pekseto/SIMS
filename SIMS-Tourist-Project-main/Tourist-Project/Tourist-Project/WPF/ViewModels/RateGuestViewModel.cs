using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class RateGuestViewModel
    {
        public Notification Notification { get; set; }
        public GuestRateViewModel GuestRate { get; set; }
        private readonly GuestRateService ratingService = new ();
        private readonly NotificationService notificationService = new ();
        public ICommand RateCommand { get; set; }
        public Window Window { get; set; }
        public RateGuestViewModel(Notification notification, Window window)
        {
            Notification = notification;
            GuestRate = new GuestRateViewModel(notification);
            RateCommand = new RelayCommand(Rate, CanRate);
            Window = window;
        }
        #region Commands
        public void Rate()
        {
            notificationService.Delete(Notification.Id);
            ratingService.Update(GuestRate.GuestRating);
            Window.Close();
        }

        public bool CanRate()
        {
            return true;
        } 
        #endregion
    }

}