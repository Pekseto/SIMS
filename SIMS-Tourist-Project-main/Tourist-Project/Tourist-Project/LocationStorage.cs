using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project
{
    public class LocationStorage
    {
        private const string fileName = "";
        private Serializer<Location> serializer;

        public LocationStorage()
        {
            serializer = new Serializer<Location>();
        }

        public List<Location> GetAll()
        {
            return serializer.FromCSV(fileName);
        }

        public void Save(List<Location> locations)
        {
            serializer.ToCSV(fileName, locations);
        }
    }
}
