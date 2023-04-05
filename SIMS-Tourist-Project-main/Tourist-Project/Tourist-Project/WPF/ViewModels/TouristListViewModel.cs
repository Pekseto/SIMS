using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Application;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repository;

namespace Tourist_Project.WPF.ViewModels
{
    public class TouristListViewModel
    {
        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        private TourPoint selectedTourPoint;

        private UserService userService = new();
        private TourPointService tourPointService = new();
        private TourAttendanceService tourAttendanceService = new();

        public ICommand CallOutCommand { get; set; }
        public TouristListViewModel(TourPoint selectedTourPoint) 
        { 
            this.selectedTourPoint = selectedTourPoint;
            CallOutCommand = new RelayCommand(CallOut, CanCallOut);
            LoadTourAttendaces();
        }

        private bool CanCallOut()
        {
            if (SelectedTourAttendance == null)
            {
                return false;
            }

            return true;
        }

        private void CallOut()
        {
            SelectedTourAttendance.TourPoint = selectedTourPoint;
            SelectedTourAttendance.CheckPointId = selectedTourPoint.Id;
            tourAttendanceService.UpdateCollection();
        }

        public void LoadTourAttendaces()
        {
            TourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceService.GetAllTourists(selectedTourPoint));

            foreach (TourAttendance attendace in TourAttendances)
            {
                attendace.User = userService.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);

            }
        }
    }
}
