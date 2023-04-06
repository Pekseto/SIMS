using System.Collections.Generic;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{ 
    public interface IGuestReviewRepository
    {
        public List<GuestReview> GetAll();
        public GuestReview Save(GuestReview guestReview);
        public int NextId();
        public void Delete(GuestReview guestReview);
        public GuestReview Update(GuestReview guestReview);
    }

}