using System;
using System.Globalization;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
	public class Reservation : ISerializable
	{
		public int Id { get; set; }
        public User guest { get; set; }
		public DateOnly CheckIn { get; set; }
		public DateOnly CheckOut { get; set; }
		public int guestsNum { get; set; }
		public int stayingDays { get; set; }
		public Accommodation accommodation { get; set; }
		public Reservation() { }
        public Reservation(DateOnly checkIn, DateOnly checkOut, int guestsNum, int stayingDays, Accommodation accommodation)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            this.guestsNum = guestsNum;
            this.stayingDays = stayingDays;
            this.accommodation = accommodation;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), guest.Id.ToString(), CheckIn.ToString(), CheckOut.ToString(), CheckOut.ToString(), guestsNum.ToString(), stayingDays.ToString(), accommodation.Id.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            guest = new User() { Id = Convert.ToInt32(values[1]) };
            CheckIn = DateOnly.Parse(values[2]);
            CheckOut = DateOnly.Parse(values[3]);
            guestsNum = Convert.ToInt32(values[4]);
            stayingDays = Convert.ToInt32(values[5]);
            accommodation = new Accommodation() { Id = Convert.ToInt32(values[6]) };
        }
    }
}