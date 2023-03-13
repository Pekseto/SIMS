using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Storage;

namespace Tourist_Project.Dao
{
    public class LocationDao
    {
        private List<IObserver> _observers;
        private LocationStorage storage;
        private List<Location> locations;

        public LocationDao()
        {
            _observers = new List<IObserver>();
            storage = new LocationStorage();
            locations = new List<Location>();
        }

        public int NextId()
        {
            //Trebalo bi da vrati dobar Id proveri sa Id koji ce se dobiti za Tour
            return storage.GetAll().Max(location => location.Id) + 1;
        }

        public void Add(Location location)
        {
            location.Id = NextId();
            locations.Add(location);
            storage.Save(locations); //Hoce li proslediti listu sa samo jednim objektom? Zbog kreiranja nove u konstruktoru
            NotifyObservers();
        }

        public List<Location> GetAll()
        {
            //TODO: Ostavljeno da se odradi posle zbog povezivanja svih klasa

            return locations;
        }
        public List<Location> GetById(int id)
        {
            return locations.FindAll(c => c.Id == id);
        }
        public void Save(List<Location> locations)
        {
            storage.Save(locations);
            this.locations = locations; //Potrebno?
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
