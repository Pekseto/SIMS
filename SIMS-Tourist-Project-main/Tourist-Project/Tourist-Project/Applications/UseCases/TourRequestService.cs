using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

        private readonly TourService tourService = new();
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
            foreach (var request in requestRepository.GetAll().Where(r => r.UserId == loggedUserId).ToList())
            {
                if (DateTime.Now < request.FromDate.AddDays(-2).Date) continue;
                request.Status = TourRequestStatus.Denied;
                requestRepository.Update(request);
            }
        }

        public List<TourRequest> GetAllPending()
        {
            return requestRepository.GetAllPending();
        }

        public bool IsAlreadyBooked(DateTime bookingDateTime, int duration)
        {
            foreach (var date in tourService.GetBookedHours())
            {
                if (CheckTourOverlap(bookingDateTime, duration, date))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckTourOverlap(DateTime bookingDateTime, int duration, DateSpan date)
        {
            return bookingDateTime.CompareTo(date.StartingDate) <= 0 &&
                   bookingDateTime.AddHours(duration).CompareTo(date.EndingDate) >= 0 || 
                   date.StartingDate.CompareTo(bookingDateTime.AddHours(duration)) <= 0 && 
                   date.EndingDate.CompareTo(bookingDateTime) >= 0;
        }

        public List<TourRequest> SearchRequests(string filter, string searchText)
        {
            switch (filter)
            {
                case "Language":
                    return GetAllPending().FindAll(request => request.Language == searchText);
                case "Location":
                {
                    var result =  searchText.Split(",");
                    var city = result[0];
                    var country = result[1];
                    var locationId = locationService.GetId(city, country);
                    return GetAllPending().FindAll(request => request.LocationId == locationId);
                }
                default:
                {
                    var result = searchText.Split("-");
                    var fromDate = DateTime.Parse(result[0]);
                    var untilDate = DateTime.Parse(result[1]);
                    return GetAllPending().FindAll(request =>
                        request.FromDate.CompareTo(fromDate) >= 0 && request.UntilDate.CompareTo(untilDate) <= 0);
                }
            }
        }

        public Location FindMostRequestedLocation()
        {
            var mostRequested = 0;
            var maxRequests = 0;
            var requestsNumber = 0;
            foreach (var location in locationService.GetAll())
            {
                    requestsNumber += requestRepository.GetAllLastYear().Count(request => request.LocationId == location.Id);

                    if (requestsNumber > maxRequests)
                    {
                        mostRequested = location.Id;
                        maxRequests = requestsNumber;
                    }

                    requestsNumber = 0;
            }
            
            return locationService.Get(mostRequested);
        }

        public string FindMOstRequestedLanguage()
        {
            var mostRequested = 0;
            var maxRequests = 0;
            var language = "";
            var requestsNumber = 0;
            foreach (var request in requestRepository.GetAll())
            {
                requestsNumber += requestRepository.GetAllLastYear().Count(tourRequest => tourRequest.Language == request.Language);

                if (requestsNumber > maxRequests)
                {
                    language = request.Language;
                    maxRequests = requestsNumber;
                }

                requestsNumber = 0;
            }
            return language;
        }

        public List<RequestStatistics> GetRequestStatistics(string filter, string search, string selectedYear)
        {
            return filter == "Location" ? GetStatisticsForLocation(search, selectedYear) : GetStatisticsForLanguage(search, selectedYear);
        }

        private List<RequestStatistics> GetStatisticsForLocation(string search, string selectedYear)
        {
            var location = search.Split(",");
            var city = location[0];
            var country = location[1];
            var locationId = locationService.GetId(city, country);
            var requestCounter = 0;

            List<RequestStatistics> statistics = new();

            statistics = selectedYear == "Overall" ? GetStatisticsForYearsByLocation(locationId) : GetStatisticsForMonthsByLocation(selectedYear, locationId);

            return statistics;
        }

        private List<RequestStatistics> GetStatisticsForMonthsByLocation(string selectedYear, int locationId)
        {
            var requestCounter = 0;
            List<RequestStatistics> statistics = new();
            var year = int.Parse(selectedYear);

            for (var i = 1; i <= 12; i++)
            {
                foreach (var request in requestRepository.GetAll()
                             .Where(request => request.CreateDate.Year == year && request.LocationId == locationId))
                {
                    if (request.CreateDate.Month == i)
                    {
                        requestCounter++;
                    }
                }

                statistics.Add(new(i.ToString(), requestCounter));
                requestCounter = 0;
            }

            return statistics;
        }

        private List<RequestStatistics> GetStatisticsForYearsByLocation(int locationId1) 
        {

            var requestCounter = 0;
            List<RequestStatistics> statistics = new();


            for (var i = DateTime.Now; i >= DateTime.Now.AddYears(-4); i = i.AddYears(-1))
            {
                requestCounter += requestRepository.GetAll()
                    .Count(request => request.CreateDate.Year == i.Year && request.LocationId == locationId1);
                statistics.Add(new(i.Year.ToString(), requestCounter));

                requestCounter = 0;
            }

            return statistics;
        }

        private List<RequestStatistics> GetStatisticsForLanguage(string search, string selectedYear)
        {
            var requestCounter = 0;
            List<RequestStatistics> statistics = new();

            if (selectedYear == "Overall")
            {
                for (var i = DateTime.Now; i >= DateTime.Now.AddYears(-4); i = i.AddYears(-1))
                {
                    requestCounter += requestRepository.GetAll().Count(request => request.Language == search && request.CreateDate.Year == i.Year);
                    statistics.Add(new(i.Year.ToString(), requestCounter)); 
                    requestCounter = 0;
                }
            }
            else
            {
                var year = int.Parse(selectedYear);
                for (var i = 1; i <= 12; i++)
                {
                    foreach (var request in requestRepository.GetAll().Where(request => request.CreateDate.Year == year && request.Language == search))
                    {
                        if (request.CreateDate.Month == i)
                        {
                            requestCounter++;

                        }
                    }
                    statistics.Add(new(i.ToString(), requestCounter));
                    requestCounter = 0;
                }
            }
            return statistics;
        }
    }
}