using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tourist_Project.DTO;
using Tourist_Project.Model;

namespace Tourist_Project.Repository
{
	public class AccommodationDTORepository
	{
		private readonly AccommodationRepository accommodationRepository;
		public AccommodationDTORepository()
		{
		}
		public List<AccommodationDTO> createDTOs(ObservableCollection<Accommodation> accommodations, ObservableCollection<Location> locations, ObservableCollection<Image> images)
		{
			List<AccommodationDTO> accommodationDTOs = new List<AccommodationDTO>();
			foreach (var accommodation in accommodations)
			{
				foreach (var location in locations)
				{
					foreach (var image in images)
					{
						if(accommodation.LocationId == location.Id && accommodation.ImageId == image.Id)
							accommodationDTOs.Add(new AccommodationDTO(accommodation, location, image));
					}
				}

			}

			return accommodationDTOs;
		}
		public void Delete(AccommodationDTO accommodationDTO)
		{
			accommodationRepository.Delete(accommodationDTO.AccommodationId);
		}

		public void Update(AccommodationDTO accommodationDTO)
        {
			accommodationRepository.Update(accommodationRepository.GetById(accommodationDTO.AccommodationId));
        }

		public void Save(AccommodationDTO accommodationDTO)
        {
			accommodationRepository.Save(accommodationRepository.GetById(accommodationDTO.AccommodationId));
        }
	}
}