using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class ComplexTourRepository : IComplexTourRepository
    {
        private const string filePath = "../../../Data/complexTours.csv";
        private readonly Serializer<ComplexTour> serializer;
        public List<ComplexTour> Tours;

        public ComplexTourRepository()
        {
            serializer = new Serializer<ComplexTour>();
            Tours = serializer.FromCSV(filePath);
        }
        public List<ComplexTour> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public List<ComplexTour> GetAllForUser(int userId)
        {
            Tours = GetAll();
            return Tours.Where(tour => tour.UserId == userId).ToList();
        }

        public ComplexTour Save(ComplexTour complexTour)
        {
            Tours = GetAll();
            complexTour.Id = NextId();
            Tours.Add(complexTour);
            serializer.ToCSV(filePath, Tours);
            return complexTour;
        }

        public int NextId()
        {
            if (Tours.Count < 1)
            {
                return 1;
            }
            return Tours.Max(t => t.Id) + 1;
        }
    }
}
