using Tourist_Project.Serializer;
using System;
using Tourist_Project.Model;
using System.Collections.Generic;

namespace Tourist_Project.Storage
{
	public class AccommodationStorage
	{
		private const string FilePath = "../../../Data/accommodations.csv";
		private readonly Serializer<Accommodation> _serializer;
		public AccommodationStorage()
		{
			_serializer = new Serializer<Accommodation>();
		}
		public List<Accommodation> Load()
        {
			return _serializer.FromCSV(FilePath);
        }
		public void Save(List<Accommodation> accommodations)
        {
			_serializer.ToCSV(FilePath, accommodations);
        }
	}
}