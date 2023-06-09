using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using Microsoft.VisualBasic.FileIO;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels.Guide
{
    public class RequestsStatisticsViewModel : INotifyPropertyChanged
    {
        private readonly TourRequestService tourRequestService = new();
        private readonly Window window;
        public User LoggedInUser { get; set; }

        private SeriesCollection statisticsCollection;
        public SeriesCollection StatisticsCollection
        {
            get { return statisticsCollection; }
            set
            {
                statisticsCollection = value;
                OnPropertyChanged("StatisticsCollection");
            }
        }


        private string currentLanguage;
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                OnPropertyChanged("CurrentLanguage");
            }
        }

        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged("Filter");
            }
        }

        private string year;
        public string Year
        {
            get {return year; }
            set
            {
                year = value; 
                OnPropertyChanged("Year");
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

        private RequestStatistics requestStatistics;
        public RequestStatistics RequestStatistics
        {
            get { return requestStatistics; }
            set
            {
                requestStatistics = value;
                OnPropertyChanged("RequestStatistics");
            }
        }

        public List<string> Filters { get; set; } = new()
        {
            "Location",
            "Language"
        };

        public List<string> Years { get; set; } 

        #region ObservableCollection

        private ObservableCollection<RequestStatistics> requestsStatistics;

        public ObservableCollection<RequestStatistics> RequestsStatistics
        {
            get { return requestsStatistics; }
            set
            {
                requestsStatistics = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public ICommand BackCommand { get; set; }
        public ICommand CreateByLocationCommand { get; set; }
        public ICommand CreateByLanguageCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ToSerbianCommand { get; set; }
        public ICommand ToEnglishCommand { get; set; }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RequestsStatisticsViewModel(Window window, User loggedInUser)
        {
            this.window = window;
            LoggedInUser = loggedInUser;
            CurrentLanguage = "en-US";

            Years = tourRequestService.GetAllYears(loggedInUser.Id);

            BackCommand = new RelayCommand(Back, CanBack);
            CreateByLocationCommand = new RelayCommand(CreateByLocation, CanCreate);
            CreateByLanguageCommand = new RelayCommand(CreateByLanguage, CanCreate);
            SearchCommand = new RelayCommand(Search, CanSearch);
            ToSerbianCommand = new RelayCommand(ToSerbian, CanToSerbian);
            ToEnglishCommand = new RelayCommand(ToEnglish, CanToEnglish);

        }

        private void GenerateCollection()
        {
            if (Filter.Equals("Location"))
            {
                StatisticsCollection = Year.Equals("Overall") ? tourRequestService.GetLocationOverallCollection(SearchBox) : tourRequestService.GenerateLocationYearCollection(SearchBox, Year);
            }
            else
            {
                StatisticsCollection = Year.Equals("Overall") ? tourRequestService.GenerateLanguageOverallCollection(SearchBox) : tourRequestService.GenerateLanguageYearCollection(SearchBox, Year);
            }
        }

        private void ToSerbian()
        {
            var app = (App)Application.Current;
            CurrentLanguage = "sr-LATN";
            app.ChangeLanguage(CurrentLanguage);
        }

        private bool CanToSerbian()
        {
            return CurrentLanguage.Equals("en-US");
        }

        private void ToEnglish()
        {
            var app = (App)Application.Current;
            CurrentLanguage = "en-US";
            app.ChangeLanguage(CurrentLanguage);
        }

        private bool CanToEnglish()
        {
            return CurrentLanguage.Equals("sr-LATN");
        }

        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            RequestsStatistics = new ObservableCollection<RequestStatistics>(tourRequestService.GetRequestStatistics(Filter, SearchBox, Year));
            GenerateCollection();
        }

        private bool CanCreate()
        {
            return true;
        }

        private void CreateByLocation()
        {
            var createWindow = new CreateTourByLocationView(LoggedInUser, tourRequestService.FindMostRequestedLocation());
            createWindow.Show();
            window.Close();
        }

        private void CreateByLanguage()
        {
            var createWindow = new CreateTourByLanguageView(LoggedInUser, tourRequestService.FindMOstRequestedLanguage());
            createWindow.Show();
            window.Close();
        }

        private bool CanBack()
        {
            return true;
        }

        private void Back()
        {
            window.Close();
        }
    }
}
