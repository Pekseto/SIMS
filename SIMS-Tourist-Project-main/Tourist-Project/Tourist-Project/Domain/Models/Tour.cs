using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum Status { NotBegin, Begin, End }
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuestsNumber { get; set; }
        List<TourPoint> tourPoints;
        public List<TourPoint> TourPoints
        {
            get => tourPoints;
            set => tourPoints = value;
        }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int ImageId { get; set; }
        public Status Status { get; set; }
        List<User> tourists;
        public List<User> Tourists
        {
            get => tourists;
            set => tourists = value;
        }
        public int UserId { get; set; }

        public Tour()
        {
            tourPoints = new List<TourPoint>();
            tourists = new List<User>();
        }

        public Tour(string name, int locationId, string description, string language, int maxGuestsNumber, DateTime startTime, int duration, int imageId)
        {
            TourPoints = new List<TourPoint>();
            tourists = new List<User>();

            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxGuestsNumber = maxGuestsNumber;
            StartTime = startTime;
            Duration = duration;
            this.ImageId = imageId;
            Status = Status.NotBegin;
        }

        #region Serilization
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                Language,
                MaxGuestsNumber.ToString(),
                StartTime.ToString(),
                Duration.ToString(),
                ImageId.ToString(),
                Status.ToString(),
                //UserId.ToString(),
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Description = values[3];
            Language = values[4];
            MaxGuestsNumber = int.Parse(values[5]);
            StartTime = DateTime.Parse(values[6]);
            Duration = int.Parse(values[7]);
            ImageId = int.Parse(values[8]);
            Status = Enum.Parse<Status>(values[9]);
            // UserId = int.Parse(values[10]);
        }
        #endregion
    }
}
