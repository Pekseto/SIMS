using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourRequestService
    {
        private static readonly Injector injector = new();

        private readonly ITourRequestRepository requestRepository = injector.CreateInstance<ITourRequestRepository>();
        public TourRequestService()
        {
            
        }

        public List<TourRequest> GetAll()
        {
            return requestRepository.GetAll();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return requestRepository.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return requestRepository.Update(tourRequest);
        }

        public void Delete(int id)
        {
            requestRepository.Delete(id);
        }

        public TourRequest GetById(int id)
        {
            return requestRepository.GetById(id);
        }

        public List<TourRequest> GetAllForUser(int userId)
        {
            return requestRepository.GetAllForUser(userId);
        }

        public void UpdateInvalidRequests(int loggedUserId)
        {
            foreach (var request in requestRepository.GetAll().Where(r => r.UserId == loggedUserId).ToList())
            {
                if (DateTime.Now < request.FromDate.AddDays(-2).Date) continue;
                request.Status = TourRequestStatus.Denied;
                requestRepository.Update(request);
            }
        }
    }
}
