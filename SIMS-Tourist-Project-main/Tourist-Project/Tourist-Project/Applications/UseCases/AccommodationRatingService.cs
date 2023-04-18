using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class AccommodationRatingService : IService<AccommodationRating>
    {
        private static readonly Injector injector = new();
        private readonly IAccommodationRatingRepository accommodationRatingRepository = injector.CreateInstance<IAccommodationRatingRepository>();

        public AccommodationRatingService()
        {
        }

        public AccommodationRating Create(AccommodationRating accommodationRating)
        {
            return accommodationRatingRepository.Save(accommodationRating);
        }

        public List<AccommodationRating> GetAll()
        {
            return accommodationRatingRepository.GetAll();
        }

        public AccommodationRating Get(int id)
        {
            return accommodationRatingRepository.GetById(id);
        }

        public void Delete(int id)
        {
            accommodationRatingRepository.Delete(id);
        }

        public AccommodationRating Update(AccommodationRating accommodationRating)
        {
            return accommodationRatingRepository.Update(accommodationRating);
        }
        public double getRating()
        {
            return (double)accommodationRatingRepository.GetAll().Sum(accommodationRating => accommodationRating.AccommodationGrade + accommodationRating.Cleanness + accommodationRating.OwnerRating) / (accommodationRatingRepository.GetAll().Count * 3);
        }
    }
}
