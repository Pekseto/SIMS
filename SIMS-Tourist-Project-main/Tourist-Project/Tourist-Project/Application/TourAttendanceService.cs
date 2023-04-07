using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repository;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Application
{
    public class TourAttendanceService
    {
        private readonly TourAttendanceRepository repository = new();
        private UserService userService = new();
        private TourPointService tourPointService = new();

        public TourAttendanceService() 
        {
        }

        public List<TourAttendance> GetAllTourists(TourPoint selectedTourPoint)
        {
            return repository.GetAllTourists(selectedTourPoint);
        }

        public void UpdateCollection(TourAttendance selectedTourAttendance, TourPoint selectedTourPoint)
        {
            var tourAttendances = TouristListViewModel.TourAttendances;
            repository.Update(selectedTourAttendance);
            tourAttendances.Clear();
            foreach (TourAttendance attendance in GetAllTourists(selectedTourPoint))
            {
                tourAttendances.Add(attendance);
            }
            foreach (TourAttendance attendace in tourAttendances)
            {
                attendace.User = userService.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);
            }
        }
    }
}
