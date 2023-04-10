using System.Collections.ObjectModel;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class OwnerMainWindowViewModel
    {
        public static ObservableCollection<Accommodation> accommodations { get; set; }
        private readonly AccommodationService accommodationService = new();
        private readonly LocationService locationService = new();
        private readonly ImageService imageService = new();
        public OwnerMainWindowViewModel()
        {
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            foreach (var accommodation in accommodations)
            {
                accommodation.Location = locationService.Get(accommodation.LocationId);
                accommodation.ImageUrl = imageService.Get(accommodation.ImageId).Url;
            }
        }
    }

}