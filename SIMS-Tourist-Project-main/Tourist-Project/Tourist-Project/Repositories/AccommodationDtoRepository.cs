using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;

namespace Tourist_Project.Repositories
{
    public class AccommodationDtoRepository
    {
        private readonly List<AccommodationDTO> accommodationDtos = new();
        public List<AccommodationDTO> LoadAll(ObservableCollection<Accommodation> accommodations, ObservableCollection<Location> locations, ObservableCollection<Image> images)
        {
            accommodationDtos.Clear();
            foreach (var accommodation in accommodations)
            {
                foreach (var location in locations)
                {
                    foreach (var image in images)
                    {
                        if (accommodation.LocationId == location.Id && accommodation.ImageId == image.Id)
                            accommodationDtos.Add(new AccommodationDTO(accommodation, location, image));
                    }
                }
            }
            return accommodationDtos;
        }
    }
}