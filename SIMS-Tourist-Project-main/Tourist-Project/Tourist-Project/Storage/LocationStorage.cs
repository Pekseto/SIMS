using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Storage
{
    public class LocationStorage
    {
        private const string FilePath = "../../../Data/locations.csv";
        private Serializer<Location> serializer;

        public LocationStorage()
        {
            serializer = new Serializer<Location>();
        }

        public List<Location> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> locations)
        {
            serializer.ToCSV(FilePath, locations);
        }
    }
}
