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
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class MyToursViewModel : ViewModelBase
    {
        private readonly TourService tourService;
        private readonly TourAttendanceService attendanceService;
        public User LoggedInUser { get; set; }
        private readonly NavigationStore navigationStore;
        public ObservableCollection<TourDTO> FutureTours { get; set; }
        public ObservableCollection<TourDTO> TodaysTours { get; set; }
        public TourDTO SelectedTodayTour { get; set; }
        public ICommand JoinCommand { get; set; }
        public ICommand WatchLiveCommand { get; set; }

        public MyToursViewModel(User user, NavigationStore navigationStore) 
        {
            LoggedInUser = user;
            this.navigationStore = navigationStore;

            tourService = new TourService();
            attendanceService = new TourAttendanceService();

            JoinCommand = new RelayCommand(OnJoinClick, CanJoin);
            WatchLiveCommand = new NavigateCommand<TourLiveGuestViewModel>(this.navigationStore, () => new TourLiveGuestViewModel(SelectedTodayTour, navigationStore), CanWatchLive);

            FutureTours = new ObservableCollection<TourDTO>(tourService.GetUsersFutureTours(MainWindow.LoggedInUser.Id));
            TodaysTours = new ObservableCollection<TourDTO>(tourService.GetUsersTodayTours(MainWindow.LoggedInUser.Id));
        }

        private bool CanJoin()
        {
            if(SelectedTodayTour != null)
            {
                TourAttendance tourAttendance = attendanceService.GetByTourIdAndUserId(SelectedTodayTour.Id, LoggedInUser.Id);
                if (SelectedTodayTour.Status == Status.Begin && tourAttendance.Presence == Presence.No)//DODATI DA NE MOZE DA JOINUJE AKO JE VEC PRISUTAN NA TURI ILI AKO JE PENDING
                {
                    return true;
                    
                }
                /*else if (SelectedTodayTour.Status != Status.Begin)
                {
                    MessageBox.Show("The tour hasn't begun yet");
                }
                else if (tourAttendance.Presence == Presence.Joined)
                {
                    MessageBox.Show("You have already joined the tour, wait for the guide to call you out");
                }
                else if (tourAttendance.Presence == Presence.Yes)
                {
                    MessageBox.Show("You are already present on the tour, click the Watch live button the follow your progress");
                }*/ //OVO CE TREBATI U HELPU DA ISPISE ZASTO NE MOZE DA SE JOINUJE 
            }
            return false;
        }

        private bool CanWatchLive()
        {
            if(SelectedTodayTour != null)
            {
                TourAttendance tourAttendance = attendanceService.GetByTourIdAndUserId(SelectedTodayTour.Id, LoggedInUser.Id);
                if(tourAttendance.Presence == Presence.Yes)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnJoinClick()
        {
            TourAttendance tourAttendance = attendanceService.GetByTourIdAndUserId(SelectedTodayTour.Id, LoggedInUser.Id);
            tourAttendance.Presence = Presence.Joined;
            attendanceService.Update(tourAttendance);
            MessageBox.Show("You have joined the tour, now you have to wait for the guide to call you out");

        }
    }
}
