using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Applications.UseCases
{
    public class TourAttendanceService
    {
        private static readonly Injector injector = new();

        private readonly ITourAttendanceRepository repository = injector.CreateInstance<ITourAttendanceRepository>();

        private readonly UserService userService = new();
        private readonly TourPointService tourPointService = new();

        public TourAttendanceService()
        {
        }

        public void UpdateCollection(TourAttendance selectedTourAttendance, TourPoint selectedTourPoint)
        {
            var tourAttendances = TouristListViewModel.TourAttendances;
            repository.Update(selectedTourAttendance);
            tourAttendances.Clear();
            foreach (var attendance in GetAllByTourId(selectedTourPoint.TourId))
            {
                tourAttendances.Add(attendance);
            }
            foreach (var attendace in tourAttendances)
            {
                attendace.User = userService.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);
            }
        }
        public List<TourAttendance> GetAllByTourId(int tourId)
        {
            return repository.GetAllByTourId(tourId);
        }

        public List<TourAttendance> GetAll()
        {
            return repository.GetAll();
        }

        public void UpdateOnUserJoined(TourAttendance tourAttendance)
        {
            repository.Update(tourAttendance);
        }

        public void Save(TourAttendance tourAttendance)
        {
            repository.Save(tourAttendance);
        }

        public void Update(TourAttendance tourAttendance)
        {
            repository.Update(tourAttendance);
        }
    }
}
