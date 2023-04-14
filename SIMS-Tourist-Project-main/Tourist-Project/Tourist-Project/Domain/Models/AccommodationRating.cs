using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class AccommodationRating : ISerializable
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        private int _accommodationId;

        public int AccommodationId
        {
            get => _accommodationId;
            set => _accommodationId = value;
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }


        private int cleanness;
        public int Cleanness
        {
            get => cleanness;
            set => cleanness = value;
        }

        private int accommodationGrade;

        public int AccommodationGrade
        {
            get => accommodationGrade;
            set => accommodationGrade = value;
        }

        private int ownerRating;
        public int OwnerRating
        {
            get => ownerRating;
            set => ownerRating = value;
        }

        private string comment;
        public string Comment 
        { 
            get => comment;
            set => comment = value;
        }

        private string imageUrl;
        public string ImageUrl 
        { 
            get => imageUrl; 
            set => imageUrl = value;
        }

        private int imageId;
        public int ImageId
        {
            get => imageId;
            set => imageId = value;
        }

        private bool notified;
        public bool Notified
        {
            get => notified;
            set => notified = value;
            
        }
        private int reservationId;
        public int ReservationId
        {
            get => reservationId;
            set => reservationId = value;
        }

        public AccommodationRating()
        {

        }

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
