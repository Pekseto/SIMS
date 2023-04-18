using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum AccommodationType { Apartment, House, Cottage }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuestNum { get; set; }
        public int MinStayingDays { get; set; }
        public int CancellationThreshold { get; set; }
        public int ImageId { get; set; }
        public int UserId { get; set; }
        public List<int> ImageIds { get; set; } = new();
        public string ImageIdsCsv { get; set; }

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
            ImageIdsCsv = imageIdes;
        }
        public string[] ToCSV()
        {
            ImageIdesToCsv();
            string[] csvValues = {
                Id.ToString(),
                UserId.ToString(),
                Name,
                LocationId.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinStayingDays.ToString(),
                CancellationThreshold.ToString(),
                ImageId.ToString(),
                ImageIdsCsv
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Name = values[2];
            LocationId = Convert.ToInt32(values[3]);
            Type = Enum.Parse<AccommodationType>(values[4]);
            MaxGuestNum = Convert.ToInt32(values[5]);
            MinStayingDays = Convert.ToInt32(values[6]);
            CancellationThreshold = Convert.ToInt32(values[7]);
            ImageId = Convert.ToInt32(values[8]);
            ImageIdsCsv = values[9];
            ImageIdesFromCsv(ImageIdsCsv);
        }
        public void ImageIdesToCsv()
        {
            if (ImageIds.Count <= 0) return;
            ImageId = ImageIds.First();
            ImageIdsCsv = string.Empty;
            foreach (var imageIde in ImageIds)
            {
                ImageIdsCsv += imageIde + ",";
            }
            ImageIdsCsv = ImageIdsCsv.Remove(ImageIdsCsv.Length - 1);
        }
        //TODO
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
