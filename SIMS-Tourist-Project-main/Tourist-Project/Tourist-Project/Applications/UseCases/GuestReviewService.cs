using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{ 
    public class GuestReviewService : IService<GuestReview>
    {
        private static readonly Injector injector = new();

        private readonly IGuestReviewRepository guestReviewRepository =
            injector.CreateInstance<IGuestReviewRepository>();
        public GuestReviewService()
        {
        }
        public GuestReview Create(GuestReview guestReview)
        {
            return guestReviewRepository.Save(guestReview);
        }
        public List<GuestReview> GetAll()
        {
            return guestReviewRepository.GetAll();
        }

        public GuestReview Get(int id)
        {
            return guestReviewRepository.GetById(id);
        }
        public GuestReview Update(GuestReview guestReview)
        {
            return guestReviewRepository.Update(guestReview);
        }
        public void Delete(int id)
        {
            guestReviewRepository.Delete(id);
        }
    }

}