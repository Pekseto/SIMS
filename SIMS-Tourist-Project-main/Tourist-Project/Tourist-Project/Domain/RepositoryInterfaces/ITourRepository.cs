using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public List<Tour> GetAll();
        public List<Tour> GetTodaysTours();
        public List<Tour> GetFutureTours();
        public List<Tour> GetPastTours();
        public void Save(Tour tour);
        public int NextId();
        public Tour Update(Tour tour);

    }
}
