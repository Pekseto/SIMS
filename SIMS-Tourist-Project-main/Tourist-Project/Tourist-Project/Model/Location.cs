using System;

namespace Tourist_Project.Model
{
	public class Location
	{
		int Id { get; set; }
		String City { get; set; }
		String Country { get; set; }

        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }
    }
}