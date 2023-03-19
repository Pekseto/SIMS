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
        int daysBeforeCancel;
        public int DaysBeforeCancel
        {
            get => daysBeforeCancel;
            set
            {
                if (value != daysBeforeCancel)
                {
                    daysBeforeCancel = value;
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
        Image image;
        public Image Image
        {
            get => image;
            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged();
                }
            }
        }
        //public int ownerId { get; set; }
        public User user { get; set; }

        List<int> imageIdes = new();
        public List<int> ImageIdes
        {
            get => imageIdes;
            set
            {
                if (value != imageIdes)
                {
                    imageIdes = value;
                    OnPropertyChanged();
                }
            }
        }
        string imageIdesCSV;
        public string ImageIdesCSV
        {
            get => imageIdesCSV;
            set
            {
                if (value != imageIdesCSV)
                {
                    imageIdesCSV = value;
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
            this.DaysBeforeCancel = daysBeforeCancel;
            this.ImageId = imageId;
            this.ImageIdesCSV = imageIdes;
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
                DaysBeforeCancel.ToString(),
                ImageId.ToString(),
                ImageIdesCSV
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
            DaysBeforeCancel = Convert.ToInt32(values[6]);
            ImageId = Convert.ToInt32(values[7]);
            ImageIdesCSV = values[8];
            ImageIdesFromCSV(ImageIdesCSV);
            //user = new User() { Id = Convert.ToInt32(values[8]) };
        }

        public void ImageIdesToCSV()
        {
            if (ImageIdes.Count > 0)
            {
                imageIdesCSV = string.Empty;
                foreach (var imageId in ImageIdes)
                {
                    ImageIdesCSV += imageId + ",";
                }
                ImageIdesCSV = ImageIdesCSV.Remove(ImageIdesCSV.Length - 1);
            }
        }
        public void ImageIdesFromCSV(string value)
        {
            var imageIdesCSV = value.Split(",");
            foreach (var imageIde in imageIdesCSV)
            {
                if (imageIde != string.Empty)
                    ImageIdes.Add(int.Parse(imageIde));
            }
        }
    }
}
