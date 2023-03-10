using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Dao;
using Tourist_Project.Model;

namespace Tourist_Project.Controller
{
    public class LocationController
    {
        private LocationDao locationDao;

        public LocationController()
        {
            locationDao = new LocationDao();
        }

        public List<Location> GetAll()
        {
            return locationDao.GetAll();
        }

        public void Save(List<Location> locations)
        {
            locationDao.Save(locations);
        }

        public void Create(Location location)
        {
            locationDao.Add(location);
        }

        public Location GetOne(int id)
        {
            return locationDao.GetAll().Find(location => location.Id == id);
        }

        public int GetId(string city, string country)
        {
            return locationDao.GetAll().Find(location => location.City == city && location.Country == country).Id;
        }
    }
}
