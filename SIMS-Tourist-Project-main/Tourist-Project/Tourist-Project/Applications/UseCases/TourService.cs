using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;

namespace Tourist_Project.Applications.UseCases
{
    public class TourService
    {

        private static readonly Injector injector = new();

        private readonly ITourRepository repository = injector.CreateInstance<ITourRepository>();

        private readonly TourReservationService tourReservationService = new();
        private readonly LocationService locationService = new();
        private readonly TourVoucherService tourVoucherService = new();

        public TourService()
        {

        }

        public List<Tour> GetAll()
        {
            return repository.GetAll();
        }

        public Tour GetOne(int id)
        {
            return repository.GetOne(id);
        }

        public void Save(Tour tour)
        {
            repository.Save(tour);
        }

        public void Update(Tour tour)
        {
            repository.Update(tour);
        }

        public int NexttId()
        {
            return repository.NextId();
        }

        public List<Tour> GetTodaysTours()
        {
            return repository.GetTodaysTours();
        }


        public List<Tour> GetFutureTours()
        {
            return repository.GetFutureTours();
        }

        public List<Tour> GetPastTours()
        {
            return repository.GetPastTours();
        }

        public List<Tour> GetAllByYear(int year, User loggedInUser)
        {
            return repository.GetAllByYear(year, loggedInUser);
        }

        public List<Tour> GetYearAppointments(string name, int year, User loggedInUser)
        {
            return repository.GetYearAppointments(name, year, loggedInUser);
        }

        public Tour GetMostVisited(int year, User loggedInUser)
        {
            var mostVisitedTour = new Tour();
            var mostTourists = 0;
            foreach (var tour in GetAllByYear(year, loggedInUser))
            {

                if (mostTourists < TouristsCountByYear(tour.Name, year, loggedInUser))
                {
                    mostTourists = TouristsCountByYear(tour.Name, year, loggedInUser);
                    mostVisitedTour = tour;
                }

            }

            return mostVisitedTour;
        }

        public int TouristsCountByYear(string tourName, int year, User loggedInUser)
        {
            var touristsCounter = 0;
            foreach (var appointment in GetYearAppointments(tourName, year, loggedInUser))
            {
                touristsCounter += tourReservationService.CountTourists(appointment.Id);
            }

            return touristsCounter;
        }

        public Tour GetOverallBest(User loggedInUser)
        {

            var mostVisitedTour = new Tour();
            var mostTourists = 0;
            foreach (var tour in GetAll())
            {

                if (mostTourists < TouristsCount(tour.Name, loggedInUser))
                {
                    mostTourists = TouristsCount(tour.Name, loggedInUser);
                    mostVisitedTour = tour;
                }

            }

            return mostVisitedTour;
        }

        public int TouristsCount(string tourName, User loggedInUser)
        {
            var touristsCounter = 0;
            foreach (var appointment in GetAllTourAppointments(tourName, loggedInUser))
            {
                touristsCounter += tourReservationService.CountTourists(appointment.Id);
            }
            return touristsCounter;
        }

        public List<Tour> GetAllTourAppointments(string tourName, User loggedInUser)
        {
            return repository.GetAllTourAppointments(tourName, loggedInUser);
        }

        public int GetLeftoverSpots(Tour tour)
        {
            var retVal = tour.MaxGuestsNumber;
            foreach (var reservation in tourReservationService.GetAll())
            {
                if (reservation.TourId == tour.Id)
                {
                    retVal -= reservation.GuestsNumber;
                }
            }
            return retVal;
        }

        public List<TourDTO> GetAllAvailableToursDto()
        {
            var tourDTOs = new List<TourDTO>();
            foreach (var tour in GetAll())
            {
                if (tour.StartTime < DateTime.Today || tour.Status != Status.NotBegin) continue;
                var tourDTO = new TourDTO(tour)
                {
                    SpotsLeft = GetLeftoverSpots(tour),
                    Location = locationService.GetLocation(tour)
                };
                tourDTOs.Add(tourDTO);
            }
            return tourDTOs;
        }

        public List<string> GetAllLanguages()
        {
            var languages = new List<string>();
            foreach (var tour in GetAll().Where(tour => !languages.Contains(tour.Language)))
            {
                languages.Add(tour.Language);
            }
            return languages;
        }

        public List<TourDTO> GetAllPastTours(int userId)
        {
            var tours = new List<TourDTO>();

            foreach (var t in tourReservationService.GetAll())
            {
                var tour = GetAll().Find(x => x.Id == t.TourId);
                if (t.UserId != userId || tour.StartTime >= DateTime.Now ||
                    (tour.Status != Status.End && tour.Status != Status.Cancel)) continue;
                
                var tourDTO = new TourDTO(tour)
                {
                    SpotsLeft = GetLeftoverSpots(tour),
                    Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                };
                tours.Add(tourDTO);
                
            }

            return tours;
        }

        public List<TourDTO> GetUsersFutureTours(int userId)
        {
            var tours = new List<TourDTO>();

            foreach (TourReservation t in tourReservationService.GetAll())
            {
                Tour tour = GetAll().Find(x => x.Id == t.TourId);
                if (t.UserId == userId && tour.StartTime >= DateTime.Today)
                {
                    var tourDTO = new TourDTO(tour)
                    {
                        SpotsLeft = GetLeftoverSpots(tour),
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    tours.Add(tourDTO);
                }
            }

            return tours;
        }

        public List<TourDTO> GetUsersTodayTours(int userId)
        {
            var tours = new List<TourDTO>();

            foreach (TourReservation t in tourReservationService.GetAll())
            {
                Tour tour = GetAll().Find(x => x.Id == t.TourId);
                if (t.UserId == userId && tour.StartTime == DateTime.Today)
                {
                    var tourDTO = new TourDTO(tour)
                    {
                        SpotsLeft = GetLeftoverSpots(tour),
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    tours.Add(tourDTO);
                }
            }

            return tours;
        }

        public List<TourDTO> GetSimilarTours(int locationId, int tourId)
        {
            var tours = new List<TourDTO>();

            foreach(Tour tour in GetAll())
            {
                if(tour.LocationId == locationId && tour.Id != tourId && tour.StartTime > DateTime.Now)
                {
                    var tourDTO = new TourDTO(tour)
                    {
                        SpotsLeft = GetLeftoverSpots(tour),
                        Location = locationService.GetAll().Find(x => x.Id == locationId)
                    };
                    tours.Add(tourDTO);
                }
            }

            return tours;
        }

        public TourDTO GetOneDTO(int tourId)
        {
            var tour = GetOne(tourId);
            var tourDTO = new TourDTO(tour)
            {
                SpotsLeft = GetLeftoverSpots(tour),
                Location = locationService.GetAll().Find(x => x.Id == tour.Id)
            };

            return tourDTO;
        }

        public List<DateSpan> GetBookedHours()
        {
            return repository.GetAllNotBegin().Select(tour => new DateSpan(tour.StartTime, tour.StartTime.AddHours(tour.Duration))).ToList();
        }

        public void CancelAllToursByGuide(int userId)
        {
            foreach (var tour in repository.GetAllNotBeginByGuide(userId))
            {
                tour.Status = Status.Cancel;
                repository.Update(tour);
                tourVoucherService.VoucherDistributionForAnyTour(tour);
            }
            
        }
    }
}
