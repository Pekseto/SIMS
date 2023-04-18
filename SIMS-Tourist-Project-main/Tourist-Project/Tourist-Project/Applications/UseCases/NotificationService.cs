using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class NotificationService
    {
        private static readonly Injector injector = new();

        private readonly INotificationRepository notificationRepository = injector.CreateInstance<INotificationRepository>();
        private readonly IAccommodationRatingRepository accommodationRatingRepository = injector.CreateInstance<IAccommodationRatingRepository>();
        private readonly IGuestRateRepository guestRateRepository = injector.CreateInstance<IGuestRateRepository>();
        private readonly IReservationRepository reservationRepository = injector.CreateInstance<IReservationRepository>();
        public NotificationService()
        {
        }

        public Notification Create(Notification notification)
        {
            return notificationRepository.Save(notification);
        }

        public List<Notification> GetAll()
        {
            return notificationRepository.GetAll();
        }

        public List<Notification> GetAllByType(string type)
        {
            return notificationRepository.GetAllByType(type);
        }

        public Notification Get(int id)
        {
            return notificationRepository.GetById(id);
        }

        public Notification Update(Notification notification)
        {
            return notificationRepository.Update(notification);
        }

        public void Delete(int id)
        {
            notificationRepository.Delete(id);
        }

        public bool HasReviews()
        {
            if (accommodationRatingRepository.GetAll().Count == 0) return false;
            foreach (var accommodationRating in accommodationRatingRepository.GetAll().Where(accommodationRating => !accommodationRating.Notified))
            {
                Create(new Notification("Reviews", accommodationRating.UserId, accommodationRating.ReservationId));
                accommodationRating.Notified = true;
                accommodationRatingRepository.Update(accommodationRating);
            }
            return true;
        }
        public void HasUnratedGuests()
        {
            foreach (var guestRate in guestRateRepository.GetAll())
            {
                foreach (var reservation in reservationRepository.GetAll())
                {
                    var daysSinceCheckOut = DateTime.Now - reservation.CheckOut;
                    if (guestRate.IsReviewed() || daysSinceCheckOut.Days >= 5 || guestRate.GuestId != reservation.GuestId) continue;
                    if (GetAllByType("GuestRate").Count == 0)
                        Create(new Notification("GuestRate", guestRate.Id, reservation.Id));
                    foreach (var notification in GetAllByType("GuestRate").Where(notification => notification.GuestRatingId != guestRate.Id && notification.ReservationId != reservation.Id))
                    {
                        Create(new Notification("GuestRate", guestRate.Id, reservation.Id));
                    }
                }
            }
        }
    }

}