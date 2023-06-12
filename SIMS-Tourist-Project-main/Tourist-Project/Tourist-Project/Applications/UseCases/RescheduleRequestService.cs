using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Domain.Models;
using System.Collections.ObjectModel;

namespace Tourist_Project.Applications.UseCases
{
    public class RescheduleRequestService
    {
        private static readonly Injector injector = new();

        private readonly IRescheduleRequestRepository rescheduleRequestRepository = injector.CreateInstance<IRescheduleRequestRepository>();


        public RescheduleRequestService()
        {
        }

        public RescheduleRequest Create(RescheduleRequest rescheduleRequest)
        {
            return rescheduleRequestRepository.Save(rescheduleRequest);
        }

        public List<RescheduleRequest> GetAll()
        {
            return rescheduleRequestRepository.GetAll();
        }

        public RescheduleRequest Get(int id)
        {
            return rescheduleRequestRepository.GetById(id);
        }

        public List<RescheduleRequest> GetByStatus(RequestStatus status)
        {
            return rescheduleRequestRepository.GetByStatus(status);
        }

        public RescheduleRequest Update(RescheduleRequest rescheduleRequest)
        {
            return rescheduleRequestRepository.Update(rescheduleRequest);
        }

        public void Delete(int id)
        {
           rescheduleRequestRepository.Delete(id);
        }

        public ObservableCollection<RescheduleRequest> GetPending()
        {
            ObservableCollection<RescheduleRequest> pendingRequests = new ObservableCollection<RescheduleRequest>();
            foreach (var rescheduleRequest in rescheduleRequestRepository.GetAll())
            {
                if(rescheduleRequest.Status == RequestStatus.Pending)
                {
                    pendingRequests.Add(rescheduleRequest);
                }
            }
            return pendingRequests;
        }

        public ObservableCollection<RescheduleRequest> GetDeclined()
        {
            ObservableCollection<RescheduleRequest> declinedRequests = new ObservableCollection<RescheduleRequest>();
            foreach (var rescheduleRequest in rescheduleRequestRepository.GetAll())
            {
                if (rescheduleRequest.Status == RequestStatus.Declined)
                {
                    declinedRequests.Add(rescheduleRequest);
                }
            }
            return declinedRequests;
        }

        public ObservableCollection<RescheduleRequest> GetAccepted()
        {
            ObservableCollection<RescheduleRequest> acceptedRequests = new ObservableCollection<RescheduleRequest>();
            foreach (var rescheduleRequest in rescheduleRequestRepository.GetAll())
            {
                if (rescheduleRequest.Status == RequestStatus.Declined)
                {
                    acceptedRequests.Add(rescheduleRequest);
                }
            }
            return acceptedRequests;
        }

    }
}
