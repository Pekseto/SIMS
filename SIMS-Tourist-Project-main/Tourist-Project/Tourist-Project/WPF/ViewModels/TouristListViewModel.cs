using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels
{
    public class TouristListViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        public TourPoint SelectedTourPoint { get; set; }
        public Tour ActiveTour { get; set; }
        public string CurrentLanguage { get; set; }

        private UserService userService = new();
        private TourPointService tourPointService = new();
        private TourAttendanceService tourAttendanceService = new();
        private NotificationGuestTwoService notificationService = new();
        private Window window;

        private DateTime currentTime;

        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CallOutCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand SwitchLanguageCommand { get; set; }

        public TouristListViewModel(TourPoint selectedTourPoint, Tour tour, Window window) 
        { 
            this.SelectedTourPoint = selectedTourPoint;
            ActiveTour = tour;
            this.window = window;
            CurrentLanguage = "en-US";

            startClock();

            CallOutCommand = new RelayCommand(CallOut, CanCallOut);
            BackCommand = new RelayCommand(Back, CanBack);
            HomeCommand = new RelayCommand(HomeView, CanHomeView);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);

            LoadTourAttendaces();
        }

        private bool CanSwitchLanguage()
        {
            return true;
        }

        public void SwitchLanguage()
        {
            var app = (App)Application.Current;
            if (CurrentLanguage.Equals("en-US"))
            {
                CurrentLanguage = "sr-LATN";
            }
            else
            {
                CurrentLanguage = "en-US";
            }
            app.ChangeLanguage(CurrentLanguage);
        }

        private void startClock()
        {
            CurrentTime = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        private bool CanBack()
        {
            return true;
        }

        private void Back()
        {
            var tourLiveWindow = new TourLiveView(ActiveTour);
            tourLiveWindow.Show();
            window.Close();
        }

        private bool CanHomeView()
        {
            return true;
        }

        private void HomeView()
        {
            window.Close();
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
                notificationService.Save(new NotificationGuestTwo(SelectedTourAttendance.UserId, SelectedTourAttendance.TourId, DateTime.Now.Date, NotificationType.ConfirmPresence));
            }
            tourAttendanceService.UpdateCollection(SelectedTourAttendance, SelectedTourPoint);
        }

        public void LoadTourAttendaces()
        {
            TourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceService.GetAllByTourId(SelectedTourPoint.TourId));

            foreach (TourAttendance attendace in TourAttendances)
            {
                attendace.User = userService.Get(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);
            }
        }
    }
}
