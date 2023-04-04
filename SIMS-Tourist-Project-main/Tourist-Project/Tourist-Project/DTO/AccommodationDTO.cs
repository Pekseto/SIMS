using Tourist_Project.Domain.Models;
using Tourist_Project.Model;

namespace Tourist_Project.DTO
{
    public class AccommodationDTO
    {
        public string Name { get; set; }
        public string LocationFullName { get; set; }
        public int MaxGuestNum { get; set; }
        public int MinStayingDays { get; set; }
        public int CancellationThreshold { get; set; }
        public string ImageUrl { get; set; }
        public int AccommodationId { get; set; }
        public AccommodationDTO()
        {
        }
        public AccommodationDTO(Accommodation accommodation, Location location, Image image)
        {
            Name = accommodation.Name;
            LocationFullName = location.City + " " + location.Country;
            MaxGuestNum = accommodation.MaxGuestNum;
            MinStayingDays = accommodation.MinStayingDays;
            CancellationThreshold = accommodation.CancellationThreshold;
            ImageUrl = image.Url;
            AccommodationId = accommodation.Id;
        }
    }
}