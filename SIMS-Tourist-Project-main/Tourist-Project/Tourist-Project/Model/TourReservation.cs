using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class TourReservation : ISerializable
    {
        private int userId;
        public int UserId
        {
            get => userId;
            set => userId = value;
        }
        private int tourId;
        public int TourId
        {
            get => tourId;
            set => tourId = value;
        }

        public TourReservation()
        {

        }
        public TourReservation(int userId, int tourId)
        {
            this.UserId = userId;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                UserId.ToString(),
                TourId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            TourId = int.Parse(values[2]);
        }
    }
}
