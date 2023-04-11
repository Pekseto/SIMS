using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Notification(){}
        public Notification(string type)
        {
            Type = type;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Type
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Type = values[1];
        }

        public override string ToString()
        {
            return Type switch
            {
                "Guest Rate" => "You have unrated guest.",
                "Forum" => "A new forum has opened. Check it out",
                "Recommended" => "You have a new recommendation.",
                "Recension" => "Guest has rated your accommodation.",
                _ => string.Empty
            };
        }
    }
}
