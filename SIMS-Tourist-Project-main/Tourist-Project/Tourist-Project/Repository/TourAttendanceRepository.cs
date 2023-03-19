using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            tourAttendance.Id = NextId();
            tourAttendances = serializer.FromCSV(filePath);
            tourAttendances.Add(tourAttendance);
            serializer.ToCSV(filePath, tourAttendances);
        }

        public int NextId()
        {
            tourAttendances = serializer.FromCSV(filePath);
            if (tourAttendances.Count < 1)
            {
                return 1;
            }
            return tourAttendances.Max(c => c.Id) + 1;
        }

        public TourAttendance Update(TourAttendance tourAttendance)
        {
            tourAttendances = serializer.FromCSV(filePath);
            TourAttendance current = tourAttendances.Find(t => t.Id == tourAttendance.Id);
            int index = tourAttendances.IndexOf(current);
            tourAttendances.Remove(current);
            tourAttendances.Insert(index, tourAttendance);
            serializer.ToCSV(filePath, tourAttendances);
            return current;
        }
    }
}
