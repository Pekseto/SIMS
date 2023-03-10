using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Dao;
using Tourist_Project.Model;

namespace Tourist_Project.Controller
{
    public class TourController
    {
        private TourDAO tourDao;

        public TourController()
        {
            tourDao = new TourDAO();
        }

        public List<Tour> GetAll()
        {
            return tourDao.GetAll();
        }

        public void Save(List<Tour> tours)
        {
            tourDao.Save(tours);
        }

        public void Create(Tour tour)
        {
            tourDao.Add(tour);
        }

        public Tour GetOne(int id)
        {
            return tourDao.GetAll().Find(tour => tour.Id == id);
        }
    }
}
