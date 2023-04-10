using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tourist_Project.Domain.Models;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum AccommodationType { Apartment, House, Cottage }
    public class Accommodation : ISerializable
    {
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }
        private int locationId;
        public int LocationId
        {
            get => locationId;
            set => locationId = value;
        }
        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if (value == location) return;
                location = value;
                OnPropertyChanged();
            }
        }
        private AccommodationType type;
        public AccommodationType Type
        {
            get => type;
            set
            {
                if (value == type) return;
                type = value;
                OnPropertyChanged();
            }
        }
        private int maxGuestNum;
        public int MaxGuestNum
        {
            get => maxGuestNum;
            set
            {
                if (value == maxGuestNum) return;
                maxGuestNum = value;
                OnPropertyChanged();
            }
        }
        private int minStayingDays;
        public int MinStayingDays
        {
            get => minStayingDays;
            set
            {
                if (value == minStayingDays) return;
                minStayingDays = value;
                OnPropertyChanged();
            }
        }
        private int cancellationThreshold;
        public int CancellationThreshold
        {
            get => cancellationThreshold;
            set
            {
                if (value == cancellationThreshold) return;
                cancellationThreshold = value;
                OnPropertyChanged();
            }
        }
        private int imageId;
        public int ImageId
        {
            get => imageId;
            set => imageId = value;
        }
        public User User { get; set; }

        private List<int> imageIds = new();
        public List<int> ImageIds
        {
            get => imageIds;
            set
            {
                if (value == imageIds) return;
                imageIds = value;
                OnPropertyChanged();
            }
        }
        private string imageIdsCSV;
        public string ImageIdsCSV
        {
            get => imageIdsCSV;
            set
            {
                if (value == imageIdsCSV) return;
                imageIdsCSV = value;
                OnPropertyChanged();
            }
        }
        public string ImageUrl 
        {
            get => imageIdsCSV;
            set
            {
                if (value == imageIdsCSV) return;
                imageIdsCSV = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Accommodation() { }
        public Accommodation(string name, int locationId, AccommodationType type, int maxGuestNum, int minStayingDays, int daysBeforeCancel, int imageId, string imageIdes)
        {
            Name = name;
            LocationId = locationId;
            Type = type;
            MaxGuestNum = maxGuestNum;
            MinStayingDays = minStayingDays;
            CancellationThreshold = daysBeforeCancel;
            ImageId = imageId;
            ImageIdsCSV = imageIdes;
        }
        public string[] ToCSV()
        {
            ImageIdesToCsv();
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinStayingDays.ToString(),
                CancellationThreshold.ToString(),
                ImageId.ToString(),
                ImageIdsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = Enum.Parse<AccommodationType>(values[3]);
            MaxGuestNum = Convert.ToInt32(values[4]);
            MinStayingDays = Convert.ToInt32(values[5]);
            CancellationThreshold = Convert.ToInt32(values[6]);
            ImageId = Convert.ToInt32(values[7]);
            ImageIdsCSV = values[8];
            ImageIdesFromCsv(ImageIdsCSV);
        }

        public void ImageIdesToCsv()
        {
            if (ImageIds.Count <= 0) return;
            ImageId = ImageIds.First();
            imageIdsCSV = string.Empty;
            foreach (var imageIde in ImageIds)
            {
                ImageIdsCSV += imageIde + ",";
            }
            ImageIdsCSV = ImageIdsCSV.Remove(ImageIdsCSV.Length - 1);
        }
        public void ImageIdesFromCsv(string value)
        {
            var imageIdesCsv = value.Split(",");
            foreach (var imageIde in imageIdesCsv)
            {
                if (imageIde != string.Empty)
                    ImageIds.Add(int.Parse(imageIde));
            }
        }
    }
}
