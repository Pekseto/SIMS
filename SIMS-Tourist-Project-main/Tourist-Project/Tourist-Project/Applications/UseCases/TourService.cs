using System;
using System.Collections.Generic;
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

        public List<Tour> GetAllByYear(int year)
        {
            return repository.GetAllByYear(year);
        }

        public List<Tour> GetYearAppointments(string name, int year)
        {
            return repository.GetYearAppointments(name, year);
        }

        public Tour GetMostVisited(int year)
        {
            var mostVisitedTour = new Tour();
            var mostTourists = 0;
            foreach (var tour in GetAllByYear(year))
            {

                if (mostTourists < TouristsCountByYear(tour.Name, year))
                {
                    mostTourists = TouristsCountByYear(tour.Name, year);
                    mostVisitedTour = tour;
                }

            }

            return mostVisitedTour;
        }

        public int TouristsCountByYear(string tourName, int year)
        {
            var touristsCounter = 0;
            foreach (var appointment in GetYearAppointments(tourName, year))
            {
                touristsCounter += tourReservationService.CountTourists(appointment.Id);
            }

            return touristsCounter;
        }

        public Tour GetOverallBest()
        {

            var mostVisitedTour = new Tour();
            var mostTourists = 0;
            foreach (var tour in GetAll())
            {

                if (mostTourists < TouristsCount(tour.Name))
                {
                    mostTourists = TouristsCount(tour.Name);
                    mostVisitedTour = tour;
                }

            }

            return mostVisitedTour;
        }

        public int TouristsCount(string tourName)
        {
            var touristsCounter = 0;
            foreach (var appointment in GetAllTourAppointments(tourName))
            {
                touristsCounter += tourReservationService.CountTourists(appointment.Id);
            }
            return touristsCounter;
        }

        public List<Tour> GetAllTourAppointments(string tourName)
        {
            return repository.GetAllTourAppointments(tourName);
        }

        public int GetLeftoverSpots(Tour tour)
        {
            int retVal = tour.MaxGuestsNumber;
            foreach (TourReservation reservation in tourReservationService.GetAll())
            {
                if (reservation.TourId == tour.Id)
                {
                    retVal -= reservation.GuestsNumber;
                }
            }
            return retVal;
        }

        public List<TourDTO> GetAllAvailableToursDTO()
        {
            var tourDTOs = new List<TourDTO>();
            foreach (Tour tour in GetAll())
            {
                if(tour.StartTime >= DateTime.Today && tour.Status == Status.NotBegin)
                {
                    var tourDTO = new TourDTO(tour)
                    {
                        SpotsLeft = GetLeftoverSpots(tour),
                        Location = locationService.GetLocation(tour)
                    };
                    tourDTOs.Add(tourDTO);
                }                
            }
            return tourDTOs;
        }

        public List<string> GetAllLanguages()
        {
            var languages = new List<string>();
            foreach (Tour tour in GetAll())
            {
                if (!languages.Contains(tour.Language))
                {
                    languages.Add(tour.Language);
                }
            }
            return languages;
        }

        public List<TourDTO> GetAllPastTours(int userId)
        {
            var tours = new List<TourDTO>();

            foreach (TourReservation t in tourReservationService.GetAll())
            {
                Tour tour = GetAll().Find(x => x.Id == t.TourId);
                if (t.UserId == userId && tour.StartTime < DateTime.Now && (tour.Status == Status.End || tour.Status == Status.Cancel))
                {
                    var tourDTO = new TourDTO(tour)
                    {
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    tours.Add(tourDTO);
                }
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
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    tours.Add(tourDTO);
                }
            }

            return tours;
        }
    }
}
