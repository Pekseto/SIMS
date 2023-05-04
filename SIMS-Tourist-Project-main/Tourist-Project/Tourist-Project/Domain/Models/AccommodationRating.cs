using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class AccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Cleanness { get; set; }
        public int AccommodationGrade { get; set; }
        public int OwnerRating { get; set; }
        public string Comment { get; set; }
        public int ImageId { get; set; }
        public int ReservationId { get; set; }
        
        public AccommodationRating() { }
        public AccommodationRating(int userId, int cleanness, string comment, int imageId, int reservationId, int ownerRating, int accommodationGrade)
        {
            UserId = userId;
            Cleanness = cleanness;
            Comment = comment;
            ImageId = imageId;
            ReservationId = reservationId;
            OwnerRating = ownerRating;
            AccommodationGrade = accommodationGrade;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                ReservationId.ToString(),
                Cleanness.ToString(),
                OwnerRating.ToString(),
                AccommodationGrade.ToString(),
                Comment,
                ImageId.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            ReservationId = Convert.ToInt32(values[2]);
            Cleanness = Convert.ToInt32(values[3]);
            OwnerRating = Convert.ToInt32(values[4]);
            AccommodationGrade = Convert.ToInt32(values[5]);
            Comment = values[6];
            ImageId = Convert.ToInt32(values[7]);
        }
    }
}
