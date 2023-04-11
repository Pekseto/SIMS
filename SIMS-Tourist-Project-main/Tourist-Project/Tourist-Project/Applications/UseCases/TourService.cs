using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

namespace Tourist_Project.Applications.UseCases
{
    public class TourService
    {

        private static readonly Injector injector = new();

        private readonly ITourRepository repository = injector.CreateInstance<ITourRepository>();

        public TourService()
        {

        }

        public List<Tour> GetAll()
        {
            return repository.GetAll();
        }

        public void Save(Tour tour)
        {
            repository.Save(tour);
        }

        public void Update(Tour tour)
        {
            repository.Update(tour);
        }
        public int NexttId()
        {
            return repository.NextId();
        }

        public List<Tour> GetTodaysTours()
        {
            return repository.GetTodaysTours();
        }


        public List<Tour> GetFutureTours()
        {
            return repository.GetFutureTours();
        }

        public List<Tour> GetPastTours()
        {
            return repository.GetPastTours();
        }
    }
}
