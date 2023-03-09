using System;

namespace Tourist_Project.Model
{
	public class Location
	{
		public int Id { get; set; }
		string City { get; set; }
		string Country { get; set; }

        public Location() { }
        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), City, Country};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}