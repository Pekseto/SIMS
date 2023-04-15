using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class TourReviewRepository : ITourReviewRepository
    {
        private const string filePath = "../../../Data/tourReviews.csv";
        private readonly Serializer<TourReview> serializer = new();
        private List<TourReview> tourReviews;

        public TourReviewRepository()
        {
            tourReviews = serializer.FromCSV(filePath);
        }

        public TourReview Save(TourReview tourReview)
        {
            tourReview.Id = NextId();
            tourReviews = serializer.FromCSV(filePath);
            tourReviews.Add(tourReview);
            serializer.ToCSV(filePath, tourReviews);
            return tourReview;
        }

        public int NextId()
        {
            tourReviews = serializer.FromCSV(filePath);
            if (tourReviews.Count < 1)
            {
                return 1;
            }
            return tourReviews.Max(c => c.Id) + 1;
        }

        public TourReview GetOne(int id)
        {
            return tourReviews.Find(t => t.Id == id);
        }

        public List<TourReview> GetAllByTourId(int id)
        {
            return tourReviews.FindAll(t => t.TourId == id);
        }

        public TourReview Update(TourReview tourReview)
        {
            tourReviews = serializer.FromCSV(filePath);
            var current = tourReviews.Find(c => c.Id == tourReview.Id);
            var index = tourReviews.IndexOf(current);
            tourReviews.Remove(current);
            tourReviews.Insert(index, tourReview);       // keep ascending order of ids in file 
            serializer.ToCSV(filePath, tourReviews);
            return tourReview;
        }
    }
}
