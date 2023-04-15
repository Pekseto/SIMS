using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int ReservationId { get; set; }
        public int GuestRatingId { get; set; }
        public Notification(){}
        public Notification(string type, int guestRatingId, int reservationId)
        {
            Type = type;
            GuestRatingId = guestRatingId;
            ReservationId = reservationId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Type,
                ReservationId.ToString(),
                GuestRatingId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Type = values[1];
            GuestRatingId = int.Parse(values[2]);
            ReservationId = int.Parse(values[2]);
        }

        public override string ToString()
        {
            return Type switch
            {
                "GuestRate" => "You have unrated guest.",
                "Forum" => "A new forum has opened. Check it out",
                "Recommended" => "You have a new \nrecommendation.",
                "Reviews" => "Guest has rated your \naccommodation.",
                _ => string.Empty
            };
        }
    }
}
