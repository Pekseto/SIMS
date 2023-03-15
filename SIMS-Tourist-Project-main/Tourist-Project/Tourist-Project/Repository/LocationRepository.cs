using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class LocationRepository
    {
        private const string filePath = "../../../Data/locations.csv";
        private readonly Serializer<Location> serializer;
        private List<Location> locations;

        public LocationRepository()
        {
            serializer = new Serializer<Location>();
            locations = serializer.FromCSV(filePath);
        }

        public List<Location> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public int GetId(string city, string country)
        {
            foreach (Location location in locations)
            {
                if (location.City == city && location.Country == country)
                    return location.Id;
            }
            return -1;
        }
        public Location GetLocation(int id)
        {
            foreach (Location location in locations)
            {
                if(location.Id == id)
                    return location;
            }
            return null;
        }
    }
}
