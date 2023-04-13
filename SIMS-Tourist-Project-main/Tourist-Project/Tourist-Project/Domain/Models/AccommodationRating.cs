using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tourist_Project.Domain.Models;
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


        private int _rating;
        public int Rating
        {
            get => _rating;

            set
            {
                if (value == _rating)
                {
                    return;
                }
                _rating = value;
                OnPropertyChanged();
            }
        }

        private int _ownerRating;
        public int OwnerRating
        {
            get => _ownerRating;
            set
            {
                if(value == _ownerRating)
                {
                    return;
                }
                _rating=value;
                OnPropertyChanged();
            }
        }
        public String Comment { get; set; }

        private int _commentId;
        public int CommentId
        {
            get => _commentId;
            set
            {
                if (_commentId == value) return;
                _commentId = value;
                OnPropertyChanged();
            }
        }

        public String ImageUrl { get; set; }

        private int _imageId;
        public int ImageId
        {
            get => _imageId;
            set
            {
                if(value == _imageId) return;
                _imageId = value;
                OnPropertyChanged();
            }
        }

        public String OwnerComment { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationRating()
        {

        }

        public AccommodationRating(int id, int accommodationId, int userId, int rating,int commentId, String comment, int imageId, String imageUrl, String ownerComment, bool notified, int reservationId, int ownerRating)
        {
            Id = id;
            AccommodationId = accommodationId;
            UserId = userId;
            Rating = rating;
            CommentId = commentId;
            Comment = comment;
            ImageId = imageId;
            ImageUrl = imageUrl;
            OwnerComment = ownerComment;
            Notified = notified;
            ReservationId = reservationId;
            OwnerRating = ownerRating;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                UserId.ToString(),
                Rating.ToString(),
                CommentId.ToString(),
                Comment,
                ImageId.ToString(),
                ImageUrl,
                Notified.ToString(),
                OwnerComment, 
                ReservationId.ToString(),
                OwnerRating.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            Rating = Convert.ToInt32(values[3]);
            CommentId = Convert.ToInt32(values[4]);
            Comment = values[5];
            ImageId = Convert.ToInt32(values[6]);
            ImageUrl = values[7];
            Notified = Convert.ToBoolean(values[8]);
            OwnerComment = values[9];
            ReservationId = Convert.ToInt32(values[10]);
            OwnerRating = Convert.ToInt32(values[11]);
        }
    }
}
