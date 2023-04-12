using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class AccommodationService : IService<Accommodation>
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
