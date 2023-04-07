using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

namespace Tourist_Project.Application
{
    public class TourService
    {
        private readonly TourRepository repository = new();
        
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

        public List<Tour> GetTodaysTours()
        {
            return repository.GetTodaysTours();
        }
    }
}
