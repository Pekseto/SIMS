using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestsNum { get; set; }
        public int StayingDays { get; set; }
        public Accommodation Accommodation { get; set; }
        public Reservation() { }
        public Reservation(DateTime checkIn, DateTime checkOut, int guestsNum, int stayingDays, Accommodation accommodation)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            this.GuestsNum = guestsNum;
            this.StayingDays = stayingDays;
            this.Accommodation = accommodation;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(), 
                GuestId.ToString(), 
                CheckIn.ToString(), 
                CheckOut.ToString(), 
                CheckOut.ToString(), 
                GuestsNum.ToString(), 
                StayingDays.ToString(), 
                Accommodation.Id.ToString() 
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            CheckIn = DateTime.Parse(values[2]);
            CheckOut = DateTime.Parse(values[3]);
            GuestsNum = Convert.ToInt32(values[4]);
            StayingDays = Convert.ToInt32(values[5]);
            Accommodation = new Accommodation() { Id = Convert.ToInt32(values[6]) };
        }
    }
}