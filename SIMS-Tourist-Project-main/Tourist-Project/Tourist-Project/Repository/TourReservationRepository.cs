using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class TourReservationRepository
    {
        private const string filePath = "../../../Data/tourReservation.csv";
        private readonly Serializer<TourReservation> serializer;
        private List<TourReservation> tourReservations;

        public TourReservationRepository()
        {
            serializer = new Serializer<TourReservation>();
            tourReservations = serializer.FromCSV(filePath);
        }

        public List<TourReservation> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public void Save(TourReservation tourReservation)
        {
            tourReservations = serializer.FromCSV(filePath);
            tourReservations.Add(tourReservation);
            serializer.ToCSV(filePath, tourReservations);
        }
    }
}
