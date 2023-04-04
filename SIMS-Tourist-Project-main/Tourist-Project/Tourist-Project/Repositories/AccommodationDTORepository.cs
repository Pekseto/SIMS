using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Model;

namespace Tourist_Project.Repository
{
    public class AccommodationDTORepository
    {
        private List<AccommodationDTO> accommodationDTOs = new();
        public AccommodationDTORepository(){ }
        public List<AccommodationDTO> LoadAll(ObservableCollection<Accommodation> accommodations, ObservableCollection<Location> locations, ObservableCollection<Image> images)
        {
            accommodationDTOs.Clear();
            foreach (var accommodation in accommodations)
            {
                foreach (var location in locations)
                {
                    foreach (var image in images)
                    {
                        if (accommodation.LocationId == location.Id && accommodation.ImageId == image.Id)
                            accommodationDTOs.Add(new AccommodationDTO(accommodation, location, image));
                    }
                }
            }
            return accommodationDTOs;
        }
    }
}