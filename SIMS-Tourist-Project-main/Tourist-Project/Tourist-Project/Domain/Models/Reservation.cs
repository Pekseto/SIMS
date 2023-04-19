using System;
using System.Collections.Generic;
using Tourist_Project.Serializer;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.Models
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestsNum { get; set; }
        public int StayingDays { get; set; }
        public int AccommodationId { get; set; } 
        public List<Date> AvailableDates { get; set; }
        public Accommodation Accommodation { get; set; } 

            
        //public List<DateTime> AvailableDates { get; set; }

        public Reservation() { }
        public Reservation(DateTime checkIn, DateTime checkOut, int guestsNum, int stayingDays, Accommodation accommodation)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            GuestsNum = guestsNum;
            StayingDays = stayingDays;
            AccommodationId = accommodation.Id;
            AvailableDates = new List<Date>();
            Accommodation = accommodation;
            //AvailableDates = new List<DateTime>();
        }
        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(), 
                GuestId.ToString(), 
                CheckIn.ToString(), 
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