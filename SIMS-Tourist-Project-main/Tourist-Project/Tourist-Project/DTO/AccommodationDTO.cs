using System;
using System.Collections.Generic;
using Tourist_Project.Model;

namespace Tourist_Project.DTO
{
    public enum AccommodationType { Apartment, House, Cottage }
    public class AccommodationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationFullName { get; set; }
        public int MaxGuestNum { get; set; }
        public int MinStayingDays { get; set; }
        public int CancellationThreshold { get; set; }
        public string ImageUrl { get; set; }
        public int AccommodationId { get; set; }

        public List<DateTime> UnavailableDates { get; set; }

        public bool IsAvailable { get; set; }

        public AccommodationType AccommodationType { get; set; }    

        public AccommodationDTO()
        {

        }
        public AccommodationDTO(Accommodation accommodation, Location location, Image image)
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            LocationFullName = location.City + " " + location.Country;
            MaxGuestNum = accommodation.MaxGuestNum;
            MinStayingDays = accommodation.MinStayingDays;
            CancellationThreshold = accommodation.CancellationThreshold;
            ImageUrl = image.Url;
            AccommodationId = accommodation.Id;
            AccommodationType = (AccommodationType)accommodation.Type;
            IsAvailable = true;
            UnavailableDates = new List<DateTime>();

        }
    }
}