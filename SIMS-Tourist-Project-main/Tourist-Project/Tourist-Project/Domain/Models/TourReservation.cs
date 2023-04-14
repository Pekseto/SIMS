using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class TourReservation : ISerializable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int userId;
        public int UserId
        {
            get => userId;
            set => userId = value;
        }
        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set { tourId = value; }
        }
        private int guestsNumber;
        public int GuestsNumber
        {
            get { return guestsNumber; }
            set { guestsNumber = value; }
        }

        private bool voucher;

        public bool Voucher
        {
            get { return voucher; }
            set { voucher = value; }
        }

        public TourReservation()
        {

        }

        public TourReservation(int userId, int tourId, int guestsNumber, bool voucher)
        {
            UserId = userId;
            TourId = tourId;
            GuestsNumber = guestsNumber;
            //ZEKO VIDI OVO ZA VAUCER
            Voucher = voucher;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                userId.ToString(),
                tourId.ToString(),
                guestsNumber.ToString(),
                voucher.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            userId = int.Parse(values[1]);
            tourId = int.Parse(values[2]);
            guestsNumber = int.Parse(values[3]);
            voucher = bool.Parse(values[4]);
        }
    }
}
