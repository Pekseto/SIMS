using System;
using System.Globalization;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
	public class Reservation : ISerializable
	{
		public int Id { get; set; }
        public int guestId { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public int guestsNum { get; set; }
		public int stayingDays { get; set; }
		public Accommodation accommodation { get; set; }
		public Reservation() { }
        public Reservation(DateTime checkIn, DateTime checkOut, int guestsNum, int stayingDays, Accommodation accommodation)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            this.guestsNum = guestsNum;
            this.stayingDays = stayingDays;
            this.accommodation = accommodation;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), guestId.ToString(), CheckIn.ToString(), CheckOut.ToString(), CheckOut.ToString(), guestsNum.ToString(), stayingDays.ToString(), accommodation.Id.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            guestId = Convert.ToInt32(values[1]);
            CheckIn = DateTime.Parse(values[2]);
            CheckOut = DateTime.Parse(values[3]);
            guestsNum = Convert.ToInt32(values[4]);
            stayingDays = Convert.ToInt32(values[5]);
            accommodation = new Accommodation() { Id = Convert.ToInt32(values[6]) };
        }
    }
}