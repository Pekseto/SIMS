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
        string fullName
        {
            get => fullName;
            set => fullName = City + " " + Country;
        }
        public Location()
        {

        }
        public Location(string city, string country)
        {
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
        public override string ToString()
        {
            return City + Country;
        }
    }
}