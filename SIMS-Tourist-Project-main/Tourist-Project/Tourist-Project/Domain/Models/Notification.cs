using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool Notified { get; set; }
        public int TypeId { get; set; }
        public Notification(){}
        public Notification(string type, bool notified, int typeId)
        {
            Type = type;
            Notified = notified;
            TypeId = typeId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Type,
                Notified.ToString(),
                TypeId.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Type = values[1];
            Notified = bool.Parse(values[2]);
            TypeId = int.Parse(values[3]);
        }
        public override string ToString()
        {
            return Type switch
            {
                "GuestRate" => "You have unrated guest.",
                "Forum" => "A new forum has opened. Check it out",
                "Recommended" => "You have a new \nrecommendation.",
                "Reviews" => "NEW!\nGuest has rated your \naccommodation.",
                _ => string.Empty
            };
        }
    }
}
