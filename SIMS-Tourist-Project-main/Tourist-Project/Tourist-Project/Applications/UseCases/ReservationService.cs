using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class ReservationService : IService<Reservation>
    {
        private static readonly Injector injector = new();

        private readonly IReservationRepository guestReviewRepository =
            injector.CreateInstance<IReservationRepository>();
        public ReservationService()
        {
        }

        public Reservation Create(Reservation reservation)
        {
            return guestReviewRepository.Save(reservation);
        }

        public List<Reservation> GetAll()
        {
            return guestReviewRepository.GetAll();
        }

        public Reservation Get(int id)
        {
            return guestReviewRepository.GetById(id);
        }

        public Reservation Update(Reservation reservation)
        {
            return guestReviewRepository.Update(reservation);
        }

        public void Delete(int id)
        {
            guestReviewRepository.Delete(id);
        }
    }

}
