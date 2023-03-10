using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Storage;

namespace Tourist_Project.DAO
{
	public class AccommodationDAO : Accommodation
	{
		private List<IObserver> _observers;
		private AccommodationStorage _storage;
		private List<Accommodation> _accommodations;
		public AccommodationDAO()
		{
			_observers = new List<IObserver>();
			_storage = new AccommodationStorage();
			_accommodations = _storage.Load();
		}
		public int NextId()
        {
			return _accommodations.Max(c => c.Id) + 1;
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }
        public void Delete(Accommodation accommodation)
        {
            _accommodations.Remove(accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }
        public void Update(Accommodation accommodation)
        {
            Accommodation current = _accommodations.Find(c => c.Id == accommodation.Id);
            int index = _accommodations.IndexOf(accommodation);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);
            _storage.Save(_accommodations);
            NotifyObservers();
        }
        public List<Accommodation> GetByUser(User user)
        {
            return _accommodations.FindAll(c => c.user.Id == user.Id);
        }
        public List<Accommodation> GetAll()
        {
            return _accommodations;
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