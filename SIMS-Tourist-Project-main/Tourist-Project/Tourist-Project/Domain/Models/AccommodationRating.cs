using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class AccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public int Cleanness { get; set; }
        public int AccommodationGrade { get; set; }
        public int OwnerRating { get; set; }
        public string Comment { get; set; }
        //TODO
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }
        public bool Notified { get; set; }
        public int ReservationId { get; set; }
        
        public AccommodationRating() { }
        //TODO
        public AccommodationRating(int id, int accommodationId, int userId, int cleanness, string comment, int imageId, string imageUrl, bool notified, int reservationId, int ownerRating, int accommodationGrade)
        {
            Id = id;
            AccommodationId = accommodationId;
            UserId = userId;
            Cleanness = cleanness;
            Comment = comment;
            ImageId = imageId;
            ImageUrl = imageUrl;
            Notified = notified;
            ReservationId = reservationId;
            OwnerRating = ownerRating;
            AccommodationGrade = accommodationGrade;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                UserId.ToString(),
                ReservationId.ToString(),
                Cleanness.ToString(),
                OwnerRating.ToString(),
                AccommodationGrade.ToString(),
                Comment,
                ImageId.ToString(),
                ImageUrl,
                Notified.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            ReservationId = Convert.ToInt32(values[3]);
            Cleanness = Convert.ToInt32(values[4]);
            OwnerRating = Convert.ToInt32(values[5]);
            AccommodationGrade = Convert.ToInt32(values[6]);
            Comment = values[7];
            ImageId = Convert.ToInt32(values[8]);
            ImageUrl = values[9];
            Notified = Convert.ToBoolean(values[10]);
        }
    }
}
