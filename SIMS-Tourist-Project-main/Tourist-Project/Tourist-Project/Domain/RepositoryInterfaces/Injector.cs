using System;
using System.Collections.Generic;
using MyNamespace;
using Tourist_Project.Repositories;

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
            {typeof(INotificationRepository), new NotificationRepository()}
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