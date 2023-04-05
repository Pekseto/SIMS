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
        private readonly TourRepository tourRepository = new();
        
        public TourService() 
        {
            
        }

        public List<Tour> GetTodaysTours()
        {
            return tourRepository.GetTodaysTours();
        }

        public void Update(Tour tour)
        {
            tourRepository.Update(tour);
        }
    }
}
