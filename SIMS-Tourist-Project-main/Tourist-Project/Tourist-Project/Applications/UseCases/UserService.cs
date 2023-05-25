using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class UserService
    {
        private static readonly Injector injector = new();

        private readonly IUserRepository repository = injector.CreateInstance<IUserRepository>();
        private readonly IAccommodationRatingRepository accommodationRatingRepository = injector.CreateInstance<IAccommodationRatingRepository>();
        private readonly AccommodationRatingService accommodationRatingService = new();
        private readonly TourService tourService = new();

        public UserService()
        {

        }
        public User GetOne(int id)
        {
            return repository.GetOne(id);
        }

        public User Update(User user)
        {
            return repository.Update(user);
        }
        public bool IsSuper(User user)
        {
            if (accommodationRatingRepository.GetAll().Count < 10 || accommodationRatingService.getRating() < 4.5)
                user.IsSuper = false;
            else
                user.IsSuper = true;
            Update(user);
            return user.IsSuper;
        }

        public void QuitJob(User user)
        {
            user.IsEmployed = false;
            Update(user);
            tourService.CancelAllToursByGuide(user.Id);
        }
    }
}
