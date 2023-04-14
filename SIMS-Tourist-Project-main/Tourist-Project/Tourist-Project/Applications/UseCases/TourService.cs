using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

namespace Tourist_Project.Applications.UseCases
{
    public class TourService
    {

        private static readonly Injector injector = new();

        private readonly ITourRepository repository = injector.CreateInstance<ITourRepository>();

        private readonly TourReservationService tourReservationService = new();

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
    }
}
