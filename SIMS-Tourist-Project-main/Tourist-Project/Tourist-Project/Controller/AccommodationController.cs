using System;
using System.Collections.Generic;
using Tourist_Project.DAO;
using Tourist_Project.Model;
using Tourist_Project.Observer;

namespace Tourist_Project.Controller
{
	public class AccommodationController
	{
		private readonly AccommodationDAO _accommodations;
		public AccommodationController()
		{
			_accommodations = new AccommodationDAO();
		}
		public List<Accommodation> GetAll()
        {
			return _accommodations.GetAll();
        }
		public void Add(Accommodation accommodation)
        {
			_accommodations.Add(accommodation);
        }
		public void Delete(Accommodation accommodation)
        {
			_accommodations.Delete(accommodation);
        }
		public void Update(Accommodation accommodation)
        {
			_accommodations.Update(accommodation);
        }
		public void Subscribe(IObserver observer)
        {
			_accommodations.Subscribe(observer);
        }
	}
}