using Tourist_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;

namespace Tourist_Project
{
    public class TourStorage
    {
        private const string fileName = "";
        private Serializer<Tour> serilizer;

        public TourStorage()
        {
            serilizer = new Serializer<Tour>();
        }

        public List<Tour> GetAll()
        {
            return serilizer.FromCSV(fileName);
        }

        public void Save(List<Tour> tours)
        {
            serilizer.ToCSV(fileName, tours);
        }
    }
}
