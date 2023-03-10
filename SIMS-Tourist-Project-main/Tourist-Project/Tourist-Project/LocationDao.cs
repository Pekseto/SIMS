using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;

namespace Tourist_Project
{
    public class LocationDao
    {
        private LocationStorage locationStorage;
        private List<Location> locations;

        public LocationDao()
        {
            locationStorage = new LocationStorage();
            locations = new List<Location>();
        }

        public int NextId()
        {
            //Trebalo bi da vrati dobar Id proveri sa Id koji ce se dobiti za Tour
            return locationStorage.GetAll().Max(location => location.Id) + 1; 
        }

        public void Add(Location location)
        {
            location.Id = NextId();
            locations.Add(location);
            locationStorage.Save(locations); //Hoce li proslediti listu sa samo jednim objektom? Zbog kreiranja nove u konstruktoru
        }

        public List<Location> GetAll()
        {
            //TODO: Ostavljeno da se odradi posle zbog povezivanja svih klasa

            return locations;
        }

        public void Save(List<Location> locations)
        {
            locationStorage.Save(locations);
            this.locations = locations; //Potrebno?
        }

    }
}
