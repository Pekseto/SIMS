using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class TourRepository : ITourRepository
    {
        private const string filePath = "../../../Data/tours.csv";
        private readonly Serializer<Tour> serializer;
        private List<Tour> tours;

        public TourRepository()
        {
            serializer = new Serializer<Tour>();
            tours = serializer.FromCSV(filePath);
        }

        public List<Tour> GetAll()
        {
            tours = serializer.FromCSV(filePath);
            return tours;
        }

        public List<Tour> GetTodaysTours()
        {
            return GetAll().FindAll(tour => tour.StartTime.Date == DateTime.Today.Date && tour.Status != Status.Cancel);
        }

        public List<Tour> GetFutureTours()
        {
            return GetAll().FindAll(t => (t.StartTime - DateTime.Now).TotalDays > 1 && t.Status != Status.Cancel);
        }

        public List<Tour> GetPastTours()
        {
            return GetAll().FindAll(tour => tour.Status == Status.End);
        }

        public void Save(Tour tour)
        {
            tour.Id = NextId();
            tours = serializer.FromCSV(filePath);
            tours.Add(tour);
            serializer.ToCSV(filePath, tours);
        }

        public int NextId()
        {
            tours = serializer.FromCSV(filePath);
            if (tours.Count < 1)
            {
                return 1;
            }
            return tours.Max(c => c.Id) + 1;
        }

        public Tour Update(Tour tour)
        {
            tours = serializer.FromCSV(filePath);
            Tour current = tours.Find(t => t.Id == tour.Id);
            int index = tours.IndexOf(current);
            tours.Remove(current);
            tours.Insert(index, tour);
            serializer.ToCSV(filePath, tours);
            return current;
        }
    }
}
