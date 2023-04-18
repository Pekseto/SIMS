using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class AccommodationService
    {
        private static readonly Injector injector = new();

        private readonly IAccommodationRepository accommodationRepository = injector.CreateInstance<IAccommodationRepository>();
        
        public AccommodationService()
        {
        }

        public Accommodation Create(Accommodation accommodation)
        {
            return accommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            /*List<Accommodation> accommodations = new List<Accommodation>(accommodationRepository.GetAll());
            List<Location> locations = new List<Location>(locationRepository.GetAll());
            foreach(Accommodation accommodation in accommodations)
            {

                accommodation.Location = locationRepository.GetById(accommodation.Id);
                
                
            }*/
            return accommodationRepository.GetAll();
        }

        public Accommodation Get(int id)
        {
            return accommodationRepository.GetById(id);
        }
        public Accommodation Update(Accommodation accommodation)
        {
            return accommodationRepository.Update(accommodation);
        }

        public void Delete(int id)
        {
            accommodationRepository.Delete(id);
        }

    }
}
