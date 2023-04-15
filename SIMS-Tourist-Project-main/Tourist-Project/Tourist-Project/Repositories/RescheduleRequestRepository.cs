using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class RescheduleRequestRepository : IRescheduleRequestRepository
    {
        private const string FilePath = "../../../Data/rescheduleRequests.csv";
        private readonly Serializer<RescheduleRequest> serializer;
        private List<RescheduleRequest> _rescheduleRequests;


        public RescheduleRequestRepository()
        {
            serializer = new Serializer<RescheduleRequest>();
            _rescheduleRequests = serializer.FromCSV(FilePath);
        }
        public List<RescheduleRequest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public RescheduleRequest Save(RescheduleRequest rescheduleRequest)
        {
            rescheduleRequest.Id = NextId();
            _rescheduleRequests = serializer.FromCSV(FilePath);
            _rescheduleRequests.Add(rescheduleRequest);
            serializer.ToCSV(FilePath, _rescheduleRequests);
            return rescheduleRequest;
        }

        public int NextId()
        {
            _rescheduleRequests = serializer.FromCSV(FilePath);
            if (_rescheduleRequests.Count < 1)
            {
                return 1;
            }
            return _rescheduleRequests.Max(c => c.Id) + 1;
        }

        public void Delete(int id)
        {
            _rescheduleRequests = serializer.FromCSV(FilePath);
            var found = _rescheduleRequests.Find(c => c.Id == id);
            _rescheduleRequests.Remove(found);
            serializer.ToCSV(FilePath, _rescheduleRequests);
        }

        public RescheduleRequest Update(RescheduleRequest rescheduleRequest)
        {
            _rescheduleRequests = serializer.FromCSV(FilePath);
            var current = _rescheduleRequests.Find(c => c.Id == rescheduleRequest.Id);
            var index = _rescheduleRequests.IndexOf(current);
            _rescheduleRequests.Remove(current);
            _rescheduleRequests.Remove(current);
            _rescheduleRequests.Insert(index, rescheduleRequest);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, _rescheduleRequests);
            return rescheduleRequest;
        }
        public RescheduleRequest GetById(int id)
        {
            return _rescheduleRequests.Find(c => c.Id == id);
        }

        public List<RescheduleRequest> GetByStatus(RequestStatus status)
        {
            return _rescheduleRequests.FindAll(c => c.Status == status);
        }


    }
}
