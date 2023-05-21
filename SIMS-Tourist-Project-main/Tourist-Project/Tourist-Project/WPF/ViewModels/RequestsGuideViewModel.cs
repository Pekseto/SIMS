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
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels
{
    public class RequestsGuideViewModel : INotifyPropertyChanged
    {
        private readonly TourRequestService tourRequestService = new();
        private readonly LocationService locationService = new();
        private Window window;

        private string pickedFilter;

        public string PickedFilter
        {
            get { return pickedFilter; }
            set
            {
                pickedFilter = value;
                OnPropertyChanged("PickedFilter");
            }
        }

        private string searchBox;

        public string SearchBox
        {
            get { return searchBox; }
            set
            {
                searchBox = value;
                OnPropertyChanged("SearchBox");
            }
        }

        private DateTime currentTime;

        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        private ObservableCollection<TourRequest> tourRequests;

        public ObservableCollection<TourRequest> TourRequests
        {
            get { return tourRequests; }
            set
            {
                tourRequests = value;
                OnPropertyChanged("TourRequests");
            }
        }

        private TourRequest selectedRequest;

        public TourRequest SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                selectedRequest = value;
                OnPropertyChanged("SelectedRequest");
            }
        }

        public string CurrentLanguage { get; set; }
        public User LoggedInUser { get; set; }

        public List<string> Filters { get; set; } = new List<string>()
        {
            "Location",
            "Date",
            "Language"
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Commands

        public ICommand SwitchLanguageCommand { get; set; }
        public ICommand ProfileViewCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }

        #endregion

        public RequestsGuideViewModel(Window window, User user)
        {
            this.window = window;
            CurrentLanguage = "en-US";
            LoggedInUser = user;

            LoadRequests();

            startClock();

            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);
            ProfileViewCommand = new RelayCommand(ProfileWindow, CanProfileWindow);
            HomeCommand = new RelayCommand(HomeWindow, CanHomeWindow);
            AcceptCommand = new RelayCommand(AcceptWindow, CanAccept);
            SearchCommand = new RelayCommand(Search, CanSearch);
            StatisticsCommand = new RelayCommand(StatisticsWindow, CanStatistics);
        }

        private bool CanStatistics()
        {
            return true;
        }

        private void StatisticsWindow()
        {
            var statisticsWindow = new RequestsStatisticsView(LoggedInUser);
            statisticsWindow.Show();
        }

        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.SearchRequests(PickedFilter, SearchBox));
            LoadRequests();
        }

        private bool CanAccept()
        {
            return SelectedRequest != null;
        }

        private void AcceptWindow()
        {
            var acceptWindow = new AcceptClassicRequestView(LoggedInUser, SelectedRequest);
            acceptWindow.Show();
        }

        private bool CanHomeWindow()
        {
            return true;
        }

        private void HomeWindow()
        {
            var homeWindow = new TodayToursView(LoggedInUser);
            homeWindow.Show();
            window.Close();
        }

        private bool CanProfileWindow()
        {
            return true;
        }

        private void ProfileWindow()
        {
            var profileWindow = new GuideProfileView(LoggedInUser);
            profileWindow.Show();
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

        public void LoadRequests()
        {
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetAllPending());
            foreach (var request in TourRequests)
            {
                request.Location = locationService.Get(request.LocationId);
            }

        }
    }
}
