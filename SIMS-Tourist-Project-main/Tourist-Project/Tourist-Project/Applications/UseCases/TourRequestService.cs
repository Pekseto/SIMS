using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourRequestService
    {
        private static readonly Injector injector = new();
        private readonly ITourRequestRepository requestRepository = injector.CreateInstance<ITourRequestRepository>();

        private readonly LocationService locationService = new();

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
            foreach (var request in requestRepository.GetAll().Where(r => r.UserId == loggedUserId && r.Status == TourRequestStatus.Pending).ToList())
            {
                if (DateTime.Now < request.UntilDate.AddDays(-2).Date) continue;
                request.Status = TourRequestStatus.Denied;
                requestRepository.Update(request);
            }
        }

        public double GetAcceptedPercentage(int userId, string statYear)
        {
            var requests = requestRepository.GetAll();
            double acceptedCount = 0;

            if (statYear == "All time")
            {
                foreach (var request in requests.Where(r => r.UserId == userId))
                {
                    if (request.Status == TourRequestStatus.Accepted)
                    {
                        acceptedCount++;
                    }
                }
                return acceptedCount > 0 ? acceptedCount / requests.Count * 100 : 0;
            }

            double selectedYearAcceptedCount = 0;
            foreach (var request in requests.Where(r => r.UserId == userId && r.FromDate.Year == int.Parse(statYear)))
            {
                selectedYearAcceptedCount++;
                if (request.Status == TourRequestStatus.Accepted)
                {
                    acceptedCount++;
                }
            }
            return acceptedCount > 0 ? acceptedCount / selectedYearAcceptedCount * 100 : 0;

        }

        public double GetDeniedPercentage(int userId, string statYear)
        {
            var requests = requestRepository.GetAll();
            double deniedCount = 0;

            if (statYear == "All time")
            {
                foreach (var request in requests.Where(r => r.UserId == userId))
                {
                    if (request.Status == TourRequestStatus.Denied)
                    {
                        deniedCount++;
                    }
                }
                return deniedCount > 0 ? deniedCount / requests.Count * 100 : 0;
            }

            double selectedYearDeniedCount = 0;
            foreach (var request in requests.Where(r => r.UserId == userId && r.FromDate.Year == int.Parse(statYear)))
            {
                selectedYearDeniedCount++;
                if (request.Status == TourRequestStatus.Denied)
                {
                    deniedCount++;
                }
            }
            return deniedCount > 0 ? deniedCount / selectedYearDeniedCount * 100 : 0;

        }

        public double GetAverageGuests(int userId, string statYear)
        {
            var requests = requestRepository.GetAll().Where(r => r.UserId == userId && r.Status == TourRequestStatus.Accepted).ToList();
            double guestsCount = 0;

            if (statYear == "All time")
            {
                foreach (var request in requests)
                {
                    guestsCount += request.GuestsNumber;
                }

                return guestsCount / requests.Count;
            }

            double selectedYearGuestsCount = 0;
            foreach (var request in requests.Where(r => r.UserId == userId && r.FromDate.Year == int.Parse(statYear)))
            {
                selectedYearGuestsCount++;
                guestsCount += request.GuestsNumber;
            } 
            return guestsCount / selectedYearGuestsCount;

        }

        public List<string> GetAllRequestedLanguages(int userId)
        {
            return requestRepository.GetAllRequestedLanguages(userId);
        }

        public int GetLanguageRequestCount(int userId, string language)
        {
            var count = 0;
            foreach (var request in requestRepository.GetAll().Where(r => r.UserId == userId))
            {
                if (request.Language == language)
                {
                    count++;
                }
            }

            return count;
        }

        public SeriesCollection GetLanguageSeriesCollection(int userId)
        {
            var retVal = new SeriesCollection();

            foreach (var requestedLanguage in GetAllRequestedLanguages(userId))
            {
                retVal.Add(new PieSeries { Title = requestedLanguage, Values = new ChartValues<ObservableValue> { new(GetLanguageRequestCount(userId, requestedLanguage)) } });
            }

            return retVal;
        }

        public List<Location> GetAllRequestedLocations(int userId)
        {
            var retVal = new List<Location>();
            foreach (var locationId in requestRepository.GetAllRequestedLocations(userId))
            {
                retVal.Add(locationService.Get(locationId));
            }

            return retVal;
        }

        public int GetLocationRequestCount(int userId, int locationId)
        {
            var count = 0;
            foreach (var request in GetAll().Where(r => r.UserId == userId))
            {
                if (request.LocationId == locationId)
                {
                    count++;
                }
            }

            return count;
        }

        public SeriesCollection GetLocationSeriesCollection(int userId)
        {
            var retVal = new SeriesCollection();

            foreach (var location in GetAllRequestedLocations(userId))
            {
                retVal.Add(new PieSeries {Title = location.ToString(), Values = new ChartValues<ObservableValue> {new(GetLocationRequestCount(userId, location.Id))}});
            }

            return retVal;
        }
    }
}
