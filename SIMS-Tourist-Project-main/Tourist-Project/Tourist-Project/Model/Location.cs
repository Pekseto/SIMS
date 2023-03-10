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
        private String city;
        public String City
        {
            get => city;
            set => city = value;
        }
        private String country;
        public String Country
        {
            get => country;
            set => country = value;
        }

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