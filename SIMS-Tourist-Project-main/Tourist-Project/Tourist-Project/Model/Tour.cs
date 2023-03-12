﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class Tour : ISerializable
    {
        int id;
        public int Id
        {
            get => id;
            set => id = value;
        }

        int locationId;
        public int LocationId
        {
            get => locationId;
            set => locationId = value;
        }

        Location location;
        public Location Location
        {
            get => location;
            set => location = value;
        }

        string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        string description;
        public string Description
        {
            get => description;
            set => description = value;
        }

        string language;
        public string Language
        {
            get => language;
            set => language = value;
        }

        int maxGuestsNumber;
        public int MaxGuestsNumber
        {
            get => maxGuestsNumber;
            set => maxGuestsNumber = value;
        }

        List<TourPoint> tourPoints;
        public List<TourPoint> TourPoints
        {
            get => tourPoints;
            set => tourPoints = value;
        }

        DateTime startTime;
        public DateTime StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        int duration;
        public int Duration
        {
            get => duration;
            set => duration = value;
        }

        int imageId;
        public int ImageId
        {
            get => imageId;
            set => imageId = value;
        }

        Image image;
        public Image Image
        {
            get => image;
            set => image = value;
        }

        public Tour()
        {
            this.tourPoints = new List<TourPoint>();
        }

        public Tour(int id, string name, int locationId, Location location, string description, string language, int maxGuestsNumber, List<TourPoint> tourPoints, DateTime startTime, int duration, Image image)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
            this.Location = location;
            this.Description = description;
            this.Language = language;
            this.MaxGuestsNumber = maxGuestsNumber;
            this.TourPoints = new List<TourPoint>();
            this.TourPoints = tourPoints;
            this.StartTime = startTime;
            this.Duration = duration;
            this.Image = image;
        }

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
                ImageId.ToString()
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
        }
    }
}