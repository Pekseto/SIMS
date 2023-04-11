using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class RateGuestViewModel
    {
        public Notification Notification { get; set; }
        public GuestRating guestRate { get; set; }
        private readonly GuestRateService ratingService = new ();
        private readonly NotificationService notificationService = new ();
        public RateGuestViewModel(Notification notification)
        {
            Notification = notification;
            guestRate = ratingService.Get(notification.GuestRatingId);
        }
    }

}