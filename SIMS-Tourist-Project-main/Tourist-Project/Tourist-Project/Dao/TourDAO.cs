using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Storage;

namespace Tourist_Project.Dao
{   //TODO: Interface for Observer
    public class TourDAO
    {
        private TourStorage tourStorage;
        private List<Tour> tours;

        public TourDAO()
        {
            tourStorage = new TourStorage();
            tours = new List<Tour>(); //Treba proslediti punu listu ne praznu!
        }

        public int NextId()
        {
            return tours.Max(tour => tour.Id) + 1;
        }

        public void Add(Tour tour)
        {
            tour.Id = NextId();
            tours.Add(tour);
            tourStorage.Save(tours);
            //TODO: NotifyObservers();
        }

        public List<Tour> GetAll()
        {
            //TODO:

            return tours;
        }

        public void Save(List<Tour> tours)
        {
            tourStorage.Save(tours);
            this.tours = tours; //Da li je ovo potrebno uopste?
            //TODO: NotifyObservers();
        }
    }
}
