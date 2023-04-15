using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FutureTours = new ObservableCollection<TourDTO>();
            TodaysTours = new ObservableCollection<TourDTO>();

            foreach(TourReservation t in reservationService.GetAll())
            {
                if(t.UserId == LoggedInUser.Id && tourService.GetAll().Find(x => x.Id == t.TourId).StartTime > DateTime.Today)
                {
                    Tour tour = tourService.GetAll().Find(x => x.Id == t.TourId);
                    var tourDTO = new TourDTO(tour)
                    {
                        Location = locationService.GetLocation(tour)
                    };
                    FutureTours.Add(tourDTO);
                }
                else if(t.UserId == LoggedInUser.Id && tourService.GetAll().Find(x => x.Id == t.TourId).StartTime == DateTime.Today)
                {
                    Tour tour = tourService.GetAll().Find(x => x.Id == t.TourId);
                    var tourDTO = new TourDTO(tour)
                    {
                        Location = locationService.GetLocation(tour)
                    };
                    TodaysTours.Add(tourDTO);
                }

            }
        }

        private void OnWatchLiveClick()
        {
            if(SelectedTodayTour.Status == Status.Begin && attendanceService.GetAllByTourId(SelectedTodayTour.Id).Find(x => x.UserId == LoggedInUser.Id).Presence == Presence.Yes)
            {
                var TourLiveGuestWindow = new TourLiveGuestView(LoggedInUser, SelectedTodayTour.Id);
                TourLiveGuestWindow.Show();
            }
        }

        private void OnJoinClick()
        {
            if(SelectedTodayTour.Status == Status.Begin )//DODATI DA NE MOZE DA JOINUJE AKO JE VEC PRISUTAN NA TURI ILI AKO JE PENDING
            {
                TourAttendance tourAttendance = attendanceService.GetAllByTourId(SelectedTodayTour.Id).Find(x => x.UserId == LoggedInUser.Id);
                tourAttendance.Presence = Presence.Joined;
                attendanceService.UpdateOnUserJoined(tourAttendance);
            }
           
        }
    }
}
