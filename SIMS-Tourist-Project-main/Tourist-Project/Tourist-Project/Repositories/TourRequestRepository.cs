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
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string filePath = "../../../Data/tourRequests.csv";
        private readonly Serializer<TourRequest> serializer;
        public List<TourRequest> Requests;

        public TourRequestRepository()
        {
            serializer = new Serializer<TourRequest>();
            Requests = serializer.FromCSV(filePath);
        }

        public List<TourRequest> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            Requests = serializer.FromCSV(filePath);
            Requests.Add(tourRequest);
            serializer.ToCSV(filePath, Requests);
            return tourRequest;
        }

        public int NextId()
        {
            Requests = serializer.FromCSV(filePath);
            if (Requests.Count < 1)
            {
                return 1;
            }
            return Requests.Max(c => c.Id) + 1;
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            Requests = serializer.FromCSV(filePath);
            var current = Requests.Find(c => c.Id == tourRequest.Id);
            var index = Requests.IndexOf(current);
            Requests.Remove(current);
            Requests.Insert(index, tourRequest);
            serializer.ToCSV(filePath, Requests);
            return tourRequest;
        }

        public void Delete(int id)
        {
            Requests = GetAll();
            var found = Requests.Find(x => x.Id == id);
            Requests.Remove(found);
            serializer.ToCSV(filePath, Requests);
        }

        public TourRequest GetById(int id)
        {
            return serializer.FromCSV(filePath).Find(tr => tr.Id == id);
        }

        public List<TourRequest> GetAllForUser(int userId)
        {
            return serializer.FromCSV(filePath).Where(tr => tr.UserId == userId).ToList();
        }
    }
}
