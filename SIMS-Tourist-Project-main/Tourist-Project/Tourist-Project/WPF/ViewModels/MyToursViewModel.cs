using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class MyToursViewModel
    {
        private readonly TourReservationService reservationService;
        private readonly TourService tourService;
        private readonly LocationService locationService;
        private readonly TourAttendanceService attendanceService;
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourDTO> FutureTours { get; set; }
        public ObservableCollection<TourDTO> TodaysTours { get; set; }
        public TourDTO SelectedTodayTour { get; set; }
        public ICommand JoinCommand { get; set; }
        public ICommand WatchLiveCommand { get; set; }

        public MyToursViewModel(User user) 
        {
            tourService = new TourService();
            reservationService = new TourReservationService();
            locationService = new LocationService();
            attendanceService = new TourAttendanceService();

            JoinCommand = new RelayCommand(OnJoinClick);
            WatchLiveCommand = new RelayCommand(OnWatchLiveClick);

            LoggedInUser = user;
            FutureTours = new ObservableCollection<TourDTO>(tourService.GetUsersFutureTours(MainWindow.LoggedInUser.Id));
            TodaysTours = new ObservableCollection<TourDTO>(tourService.GetUsersTodayTours(MainWindow.LoggedInUser.Id));
        }

        private void OnWatchLiveClick()
        {
            TourAttendance tourAttendance = attendanceService.GetByTourIdAndUserId(SelectedTodayTour.Id, MainWindow.LoggedInUser.Id);
            if (SelectedTodayTour.Status == Status.Begin && tourAttendance.Presence == Presence.Yes)
            {
                var TourLiveGuestWindow = new TourLiveGuestView(SelectedTodayTour);
                TourLiveGuestWindow.Show();                
            }
            else
            {
                MessageBox.Show("First you have to join the tour, then wait for the guide to call you out before you can watch the tour");
            }
        }

        private void OnJoinClick()
        {
            TourAttendance tourAttendance = attendanceService.GetByTourIdAndUserId(SelectedTodayTour.Id, MainWindow.LoggedInUser.Id);
            if (SelectedTodayTour.Status == Status.Begin && tourAttendance.Presence == Presence.No)//DODATI DA NE MOZE DA JOINUJE AKO JE VEC PRISUTAN NA TURI ILI AKO JE PENDING
            {
                tourAttendance.Presence = Presence.Joined;
                attendanceService.Update(tourAttendance);
                MessageBox.Show("You have joined the tour, now you have to wait for the guide to call you out");
            }
            else if(SelectedTodayTour.Status != Status.Begin)
            {
                MessageBox.Show("The tour hasn't begun yet");
            }
            else if(tourAttendance.Presence == Presence.Joined)
            {
                MessageBox.Show("You have already joined the tour, wait for the guide to call you out");
            }
            else if(tourAttendance.Presence == Presence.Yes)
            {
                MessageBox.Show("You are already present on the tour, click the Watch live button the follow your progress");
            }
           
        }
    }
}
