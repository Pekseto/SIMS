using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Threading;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels
{
    public class TodayToursViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
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
        public Tour SelectedTour { get; set; } 
        public static bool Live { get; set; }
        private readonly TourService tourService = new();
        private readonly Window window;
        private string CurrentLanguage;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Command
        public ICommand CreateCommand { get; set; }
        public ICommand StartTourCommand { get; set; }
        public ICommand FutureToursCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand RequestsCommand { get; set; }
        public ICommand ProfileViewCommand { get; set; }
        public ICommand SwitchLanguageCommand { get; set; }
        #endregion
        public TodayToursViewModel(Window window)
        {
            TodayTours = new ObservableCollection<Tour>(tourService.GetTodaysTours());

            SelectedTour = new Tour();
            this.window = window;
            Live = false;
            CurrentLanguage = "en - US";

            startClock();

            CreateCommand = new RelayCommand(CreateTour, CanCreateTour);
            StartTourCommand = new RelayCommand(StartTour, CanStartTour);
            FutureToursCommand = new RelayCommand(FutureTours, CanFutureTours);
            HistoryCommand = new RelayCommand(History, CanHistory);
            RequestsCommand = new RelayCommand(Requests, CanRequests);
            ProfileViewCommand = new RelayCommand(ProfileView, CanProfileView);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);
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

        private bool CanProfileView()
        {
            return true;
        }

        public void ProfileView()
        {
            var profileWindow = new GuideProfileView();
            profileWindow.Owner = window;
            profileWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            profileWindow.Show();
        }

        private bool CanRequests()
        {
            return true;
        }

        private void Requests()
        {
            var requestsWindow = new RequestsGuideView();
            requestsWindow.Show();
            window.Close();
        } 

        private bool CanHistory()
        {
            return true;
        }

        private void History()
        {
            var historyWindow = new HistoryOfToursView();
            historyWindow.Show();
            window.Close();
        }

        private bool CanFutureTours()
        {
            return true;
        }

        private void FutureTours()
        {
            var futureWindow = new FutureToursView();
            futureWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            futureWindow.Show();
            window.Close();
        }

        private bool CanCreateTour()
        {
            return true;
        }
        private void CreateTour()
        {
            var createTourWindow = new CreateTourView();
            createTourWindow.Owner = window;
            createTourWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createTourWindow.Show();
        }

        private bool CanStartTour()
        {
            if (Live)
            {
                return SelectedTour.Status == Status.Begin;
            }
            else
            {
                return SelectedTour.Status != Status.End && SelectedTour is not null;
            }
        }
        private void StartTour()
        {
            Live = true;
            SelectedTour.Status = Status.Begin;
            tourService.Update(SelectedTour);

            var tourLiveWindow = new TourLiveView(SelectedTour);
            tourLiveWindow.ShowDialog();
            tourLiveWindow.Close();

            tourService.Update(SelectedTour);
        }
    }
}
