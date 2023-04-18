using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class AccommodationViewModel
    {
        public Accommodation Accommodation { get; set; }
        public Location Location { get; set; }
        public Image Image { get; set; }
        private readonly LocationService locationService = new();
        private readonly ImageService imageService = new();
        public AccommodationViewModel() { }

        public AccommodationViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
            Location = locationService.Get(accommodation.LocationId);
            Image = imageService.Get(accommodation.ImageId);
        }
    }

}