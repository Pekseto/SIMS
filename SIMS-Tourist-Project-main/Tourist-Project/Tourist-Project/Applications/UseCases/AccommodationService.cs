using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class AccommodationService
    {
        private static readonly Injector injector = new();

        private readonly IAccommodationRepository accommodationRepository = injector.CreateInstance<IAccommodationRepository>();
        private readonly RenovationService renovationService = new();
        
        public AccommodationService()
        {
            IsRecentlyRenovated();
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

        public List<Accommodation> GetByUser(int userId)
        {
            return accommodationRepository.GetByUser(userId);
        }

        public List<int> GetLocationsIds(int userId)
        {
            return accommodationRepository.GetLocationIds(userId);
        }

        public void IsRecentlyRenovated()
        {
            foreach (var accommodation in GetAll())
            {
                foreach (var renovation in renovationService.GetAll()
                             .Where(renovation => renovation.AccommodationId == accommodation.Id))
                {
                    if ((DateTime.Now - renovation.RenovatingSpan.EndingDate).Days is < 365 and > 0)
                    {
                        accommodation.IsRecentlyRenovated = true;
                        Update(accommodation);
                    }
                    else
                    {
                        accommodation.IsRecentlyRenovated = false;
                        Update(accommodation);
                    }
                }
            }
        }

    }
}
