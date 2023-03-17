using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Serializer;
using System.Threading.Tasks;

namespace Tourist_Project.Repository
{
	public class AccommodationRepository : Subject
	{
        private const string FilePath = "../../../Data/accommodations.csv";
        private readonly Serializer<Accommodation> serializer;
        private List<Accommodation> accommodations;
        private readonly List<IObserver> _observers;
        //private 
        public AccommodationRepository()
        {
            serializer = new Serializer<Accommodation>();
            accommodations = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }
        public List<Accommodation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            accommodations = serializer.FromCSV(FilePath);
            accommodations.Add(accommodation);
            serializer.ToCSV(FilePath, accommodations);
            return accommodation;
        }

        public int NextId()
        {
            accommodations = serializer.FromCSV(FilePath);
            if (accommodations.Count < 1)
            {
                return 1;
            }
            return accommodations.Max(c => c.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            accommodations = serializer.FromCSV(FilePath);
            Accommodation founded = accommodations.Find(c => c.Id == accommodation.Id);
            accommodations.Remove(founded);
            serializer.ToCSV(FilePath, accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            accommodations = serializer.FromCSV(FilePath);
            Accommodation current = accommodations.Find(c => c.Id == accommodation.Id);
            int index = accommodations.IndexOf(current);
            accommodations.Remove(current);
            accommodations.Insert(index, accommodation);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, accommodations);
            return accommodation;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
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