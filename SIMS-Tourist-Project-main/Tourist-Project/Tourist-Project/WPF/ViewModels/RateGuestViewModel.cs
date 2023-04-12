using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class RateGuestViewModel
    {
        public  Notification notification { get; set; }
        public GuestRating guestRate { get; set; }
        private readonly GuestRateService ratingService = new ();
        private static readonly NotificationService NotificationService = new ();
        public ICommand RateCommand { get; set; }
        public Window Window { get; set; }
        public RateGuestViewModel(Notification notification, Window window)
        {
            this.notification = notification;
            guestRate = ratingService.Get(notification.GuestRatingId);
            RateCommand = new RelayCommand(Rate, CanRate);
            Window = window;
        }

        public void Rate()
        {
            NotificationService.Delete(notification.Id);
            ratingService.Update(guestRate);
            Window.Close();
        }

        public static bool CanRate()
        {
            return true;
        }
    }

}