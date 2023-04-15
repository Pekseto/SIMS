using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.WPF.ViewModels
{
    public class TouristListViewModel
    {
        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        public TourPoint SelectedTourPoint { get; set; }

        private UserService userService = new();
        private TourPointService tourPointService = new();
        private TourAttendanceService tourAttendanceService = new();

        public ICommand CallOutCommand { get; set; }
        public TouristListViewModel(TourPoint selectedTourPoint) 
        { 
            this.SelectedTourPoint = selectedTourPoint;
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
            SelectedTourAttendance.TourPoint = SelectedTourPoint;
            SelectedTourAttendance.CheckPointId = SelectedTourPoint.Id;
            if(SelectedTourAttendance.Presence == Presence.Joined)
            {
                SelectedTourAttendance.Presence = Presence.Pending;
            }
            tourAttendanceService.UpdateCollection(SelectedTourAttendance, SelectedTourPoint);
        }

        public void LoadTourAttendaces()
        {
            TourAttendances = new(tourAttendanceService.GetAllByTourId(SelectedTourPoint.TourId));

            foreach (TourAttendance attendace in TourAttendances)
            {
                attendace.User = userService.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);
            }
        }
    }
}
