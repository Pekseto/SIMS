using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class TourAttendance : ISerializable
    {
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

        }

        public TourAttendance(int userId, int tourId)
        {
            this.userId = userId;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                        {
                UserId.ToString(),
                TourId.ToString(),
                CheckPointId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            CheckPointId = int.Parse(values[2]);
        }
    }
}
