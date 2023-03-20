using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public enum AccommodationType { Apartment, House, Cottage }
    public class Accommodation : ISerializable
    {
        int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
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
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }
        AccommodationType type;
        public AccommodationType Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }
        int maxGuestNum;
        public int MaxGuestNum
        {
            get => maxGuestNum;
            set
            {
                if (value != maxGuestNum)
                {
                    maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }
        int minStayingDays;
        public int MinStayingDays
        {
            get => minStayingDays;
            set
            {
                if (value != minStayingDays)
                {
                    minStayingDays = value;
                    OnPropertyChanged();
                }
            }
        }
        int cancellationThreshold;
        public int CancellationThreshold
        {
            get => cancellationThreshold;
            set
            {
                if (value != cancellationThreshold)
                {
                    cancellationThreshold = value;
                    OnPropertyChanged();
                }
            }
        }
        int imageId;
        public int ImageId
        {
            get => imageId;
            set => imageId = value;
        }
        public User user { get; set; }

        List<int> imageIds = new();
        public List<int> ImageIds
        {
            get => imageIds;
            set
            {
                if (value != imageIds)
                {
                    imageIds = value;
                    OnPropertyChanged();
                }
            }
        }
        string imageIdsCSV;
        public string ImageIdsCSV
        {
            get => imageIdsCSV;
            set
            {
                if (value != imageIdsCSV)
                {
                    imageIdsCSV = value;
                    OnPropertyChanged();
                }
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
            this.Type = type;
            this.MaxGuestNum = maxGuestNum;
            this.MinStayingDays = minStayingDays;
            this.CancellationThreshold = daysBeforeCancel;
            this.ImageId = imageId;
            this.ImageIdsCSV = imageIdes;
        }
        public string[] ToCSV()
        {
            ImageIdesToCSV();
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
            ImageIdesFromCSV(ImageIdsCSV);
        }

        public void ImageIdesToCSV()
        {
            if (ImageIds.Count > 0)
            {
                imageIdsCSV = string.Empty;
                foreach (var imageId in ImageIds)
                {
                    ImageIdsCSV += imageId + ",";
                }
                ImageIdsCSV = ImageIdsCSV.Remove(ImageIdsCSV.Length - 1);
            }
        }
        public void ImageIdesFromCSV(string value)
        {
            var imageIdesCSV = value.Split(",");
            foreach (var imageIde in imageIdesCSV)
            {
                if (imageIde != string.Empty)
                    ImageIds.Add(int.Parse(imageIde));
            }
        }
    }
}
