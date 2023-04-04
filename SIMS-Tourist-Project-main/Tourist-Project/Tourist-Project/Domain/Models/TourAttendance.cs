using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class TourAttendance : ISerializable
    {
        private int id;
        public int Id { get; set; }
        private int userId;
        public int UserId { get; set; }
        private int tourId;
        public int TourId { get; set; }
        private int checkpointId;
        public int CheckPointId { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public TourPoint TourPoint { get; set; }

        public TourAttendance()
        {
            TourPoint = new TourPoint();
            CheckPointId = -1;
        }

        public TourAttendance(int userId, int tourId)
        {
            this.userId = userId;
            TourId = tourId;
            CheckPointId = -1;
            TourPoint = new TourPoint();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                        {
                Id.ToString(),
                UserId.ToString(),
                TourId.ToString(),
                CheckPointId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            CheckPointId = int.Parse(values[3]);
        }
    }
}
