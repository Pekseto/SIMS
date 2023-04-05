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

        public TourAttendanceService() 
        {
            
        }

        public List<TourAttendance> GetAllTourists(TourPoint selectedTourPoint)
        {
            return repository.GetAllTourists(selectedTourPoint);
        }

        public void UpdateCollection()
        {
            var tourAttendances = TouristListViewModel.TourAttendances;
            repository.Update(SelectedTourAttendance);
            tourAttendances.Clear();
            foreach (TourAttendance attendance in GetAllTourists())
            {
                tourAttendances.Add(attendance);
            }
            foreach (TourAttendance attendace in tourAttendances)
            {
                attendace.User = userRepository.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointRepository.GetOne(attendace.CheckPointId);
            }
        }
    }
}
