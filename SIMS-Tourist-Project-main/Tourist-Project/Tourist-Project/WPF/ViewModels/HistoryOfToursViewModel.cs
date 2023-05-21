using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels
{
    public class HistoryOfToursViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        
        private TourService tourService = new();
        private Window window;
        private DateTime currentTime;

        public Tour SelectedTour { get; set; }

        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged();
            }
        }

        public string CurrentLanguage { get; set; }
        public User LoggedInUser { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Command
        public ICommand HomeViewCommand { get; set; }
        public ICommand FutureViewCommand { get; set; }
        public ICommand StatisticsViewCommand { get; set; }
        public ICommand ReviewViewCommand { get; set; }
        public ICommand ProfileViewCommand { get; set; }
        public ICommand RequestsViewCommand { get; set; }
        public ICommand SwitchLanguageCommand { get; set; }
        #endregion
        public HistoryOfToursViewModel(Window window, User loggedInUser) 
        {
            LoggedInUser = loggedInUser;
            this.window = window;
            SelectedTour = null;
            CurrentLanguage = "en-US";
            Tours = new ObservableCollection<Tour>(tourService.GetPastTours());

            startClock();

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            FutureViewCommand = new RelayCommand(FutureView, CanFutureView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);
            ReviewViewCommand = new RelayCommand(ReviewView, CanReviewView);
            ProfileViewCommand = new RelayCommand(ProfileView, CanProfileView);
            RequestsViewCommand = new RelayCommand(RequestsView, CanRequestsView);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);
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
            timer.Tick += tickEvent;
            timer.Start();
        }

        private void tickEvent(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        private bool CanRequestsView()
        {
            return true;
        }

        public void RequestsView()
        {
            var requestsWindow = new RequestsGuideView(LoggedInUser);
            requestsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            requestsWindow.Show();
            window.Close();
        }

        private bool CanProfileView()
        {
            return true;
        }

        public void ProfileView()
        {
            var profileWindow = new GuideProfileView(LoggedInUser);
            profileWindow.Owner = window;
            profileWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            profileWindow.Show();
        }

        private bool CanHomeView()
        {
            return true;
        }

        public void HomeView()
        {
            var homeWindow = new TodayToursView(LoggedInUser);
            homeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            homeWindow.Show();
            window.Close();
        }

        private bool CanFutureView()
        {
            return true;
        }

        public void FutureView()
        {
            var futureWindow = new FutureToursView(LoggedInUser);
            futureWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            futureWindow.Show();
            window.Close();
        }

        private bool CanStatisticsView()
        {
            if(SelectedTour == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void StatisticsView()
        {
            var statisticsWindow = new StatisticsOfTourView(SelectedTour);
            statisticsWindow.Owner = window;
            statisticsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            statisticsWindow.Show();
        }

        private bool CanReviewView()
        {
            if (SelectedTour is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ReviewView()
        {
            var reviewWindow = new TourReviewsGuideView(SelectedTour);
            reviewWindow.Owner = window;
            reviewWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            reviewWindow.Show();
        }
    }
}
