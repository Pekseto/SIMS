using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class GuestReviewRepository : IGuestReviewRepository
    {
        private const string FilePath = "../../../Data/guestReviews.csv";
        private readonly Serializer<GuestReview> serializer;
        private List<GuestReview> guestReviews;
        public GuestReviewRepository()
        {
            serializer = new Serializer<GuestReview>();
            guestReviews = serializer.FromCSV(FilePath);
        }
        public List<GuestReview> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            guestReview.Id = NextId();
            guestReviews = serializer.FromCSV(FilePath);
            guestReviews.Add(guestReview);
            serializer.ToCSV(FilePath, guestReviews);
            return guestReview;
        }

        public int NextId()
        {
            guestReviews = serializer.FromCSV(FilePath);
            if (guestReviews.Count < 1)
            {
                return 1;
            }
            return guestReviews.Max(c => c.Id) + 1;
        }

        public void Delete(int id)
        {
            guestReviews = serializer.FromCSV(FilePath);
            var founded = guestReviews.Find(c => c.Id == id);
            guestReviews.Remove(founded);
            serializer.ToCSV(FilePath, guestReviews);
        }

        public GuestReview Update(GuestReview guestReview)
        {
            guestReviews = serializer.FromCSV(FilePath);
            var current = guestReviews.Find(c => c.Id == guestReview.Id);
            var index = guestReviews.IndexOf(current);
            guestReviews.Remove(current);
            guestReviews.Insert(index, guestReview);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, guestReviews);
            return guestReview;
        }

        public GuestReview GetById(int id)
        {
            return guestReviews.Find(c => c.Id == id);
        }
    }
}