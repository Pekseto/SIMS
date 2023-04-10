using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourReservationService
    {
        private static readonly Injector injector = new();
        private readonly ITourReservationRepository repository;

        public TourReservationService()
        {
            repository = injector.CreateInstance<ITourReservationRepository>();
        }

        public List<TourReservation> GetAll()
        {
            return repository.GetAll();
        }
        public void Save(TourReservation reservation)
        {
            repository.Save(reservation);
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }
        public void Update(TourReservation reservation)
        {
            repository.Update(reservation);
        }
        public TourReservation GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
