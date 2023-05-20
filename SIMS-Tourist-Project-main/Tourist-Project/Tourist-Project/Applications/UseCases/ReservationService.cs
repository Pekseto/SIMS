using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class ReservationService
    {
        private static readonly Injector injector = new();

        private readonly IReservationRepository reservationRepository =
            injector.CreateInstance<IReservationRepository>();


        public ReservationService()
        {
        }

        public Reservation Create(Reservation reservation)
        {
            return reservationRepository.Save(reservation);
        }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }

        public List<Reservation> GetAllByAccommodation(int accommodationId)
        {
            return GetAll().Where(reservation => reservation.AccommodationId == accommodationId).ToList();
        }

        public Reservation Get(int id)
        {
            return reservationRepository.GetById(id);
        }

        public Reservation Update(Reservation reservation)
        {
            return reservationRepository.Update(reservation);
        }

        public void Delete(int id)
        {
            reservationRepository.Delete(id);
        }

        public List<Reservation> FindReservationsForAccommodation(Accommodation selectedAccommodation)
        {
            List<Reservation> reservationsForAccommodation = reservationRepository.GetByAccommodation(selectedAccommodation);
            return reservationsForAccommodation;
        }

        public List<Reservation> GetAllOrderedInDateSpan(DateSpan dateSpan)
        {
            return reservationRepository.GetAll().Where(reservation =>
                (reservation.CheckIn > dateSpan.StartingDate && reservation.CheckIn < dateSpan.EndingDate) ||
                (reservation.CheckOut > dateSpan.StartingDate && reservation.CheckOut < dateSpan.EndingDate)).OrderBy(o=>o.CheckIn).ToList();
        }
    }

}
