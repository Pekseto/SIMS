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

        private readonly ITourReservationRepository repository = injector.CreateInstance<ITourReservationRepository>();

        public TourReservationService()
        {

        }

        public List<TourReservation> GetAllForTour(int id)
        {
            return repository.GetAll().FindAll(a => a.TourId == id);
        }

    }
}
