using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum Status { NotBegin, Begin, End, Cancel }
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuestsNumber { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int ImageId { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }

        public Tour()
        {
        }

        public Tour(string name, int locationId, string description, string language, int maxGuestsNumber, DateTime startTime, int duration, int imageId, int userId)
        {
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxGuestsNumber = maxGuestsNumber;
            StartTime = startTime;
            Duration = duration;
            this.ImageId = imageId;
            Status = Status.NotBegin;
            UserId = userId;
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
                UserId.ToString(),
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
            UserId = int.Parse(values[10]);
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
