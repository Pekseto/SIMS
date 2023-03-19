using System.Collections.Generic;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class TourAttendanceRepository
    {
        private const string filePath = "../../../Data/tourAttendance.csv";
        private readonly Serializer<TourAttendance> serializer;
        private List<TourAttendance> tourAttendances;

        public TourAttendanceRepository()
        {
            serializer = new Serializer<TourAttendance>();
            tourAttendances = serializer.FromCSV(filePath);
        }

        public List<TourAttendance> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public void Save(TourAttendance tourAttendance)
        {
            tourAttendances = serializer.FromCSV(filePath);
            tourAttendances.Add(tourAttendance);
            serializer.ToCSV(filePath, tourAttendances);
        }
    }
}
