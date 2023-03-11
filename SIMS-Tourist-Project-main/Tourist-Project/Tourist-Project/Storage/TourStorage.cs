using Tourist_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;

namespace Tourist_Project.Storage
{
    public class TourStorage
    {
        private const string fileName = "\\...\\...\\SIMS-Tourist-Project-main\\Tourist-Project\\Tourist-Project\\Data\\tours.csv";
        private Serializer<Tour> serializer;

        public TourStorage()
        {
            serializer = new Serializer<Tour>();
        }

        public List<Tour> GetAll()
        {
            return serializer.FromCSV(fileName);
        }

        public void Save(List<Tour> tours)
        {
            serializer.ToCSV(fileName, tours);
        }
    }
}
