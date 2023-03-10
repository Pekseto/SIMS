using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
	public class Location : ISerializable
    {
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
		String City { get; set; }
		String Country { get; set; }

        public Location()
        {

        }

        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}