using System;
using System.Collections.Generic;
using MyNamespace;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public class Injector
    {
        private static Dictionary<Type, object> implementations = new()
        {
            {typeof(IAccommodationRepository), new AccommodationRepository()},
            {typeof(IGuestRateRepository), new GuestRateRepository()},
            {typeof(ILocationRepository), new LocationRepository()},
            {typeof(IImageRepository), new ImageRepository()},
            {typeof(IReservationRepository), new ReservationRepository()},
            {typeof(ITourRepository), new TourRepository()},
            {typeof(ITourPointRepository), new TourPointRepository()},
            {typeof(ITourAttendanceRepository), new TourAttendanceRepository()},
            {typeof(ITourVoucherRepository), new TourVoucherRepository()},
            {typeof(ITourReservationRepository), new TourReservationRepository()},
            {typeof(IVoucherRepository), new VoucherRepository()},
            {typeof(IUserRepository), new UserRepository()},
            {typeof(INotificationRepository), new NotificationRepository()},
<<<<<<< HEAD
            {typeof(IAccommodationRatingRepository), new AccommodationRatingRepository()}
=======
            {typeof(IAccommodationRatingRepository), new AccommodationRatingRepository()},
            {typeof(ITourReviewRepository), new TourReviewRepository()}
>>>>>>> 6e8d6dd7877002bfbbd2df3577809d16e79e8332
        };

        public T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (implementations.ContainsKey(type))
            {
                return (T)implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}