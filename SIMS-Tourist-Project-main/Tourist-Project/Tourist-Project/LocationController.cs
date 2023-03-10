using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;

namespace Tourist_Project
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
            foreach(Location location in locationDao.GetAll())
            {
                if(location.Id == id)
                    return location;
            }
            return null;
        }
    }
}
