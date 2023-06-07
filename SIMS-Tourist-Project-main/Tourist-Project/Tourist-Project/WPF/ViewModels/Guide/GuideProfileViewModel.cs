using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels.Guide
{
    public class GuideProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Years { get; set; } = new();
        private readonly TourService tourService = new ();
        private readonly ImageService imageService = new();
        private readonly UserService userService = new ();
        private readonly TourReviewService reviewService = new ();
        private readonly Window window;
        public Tour Tour { get; set; }
        public User LoggedInUser { get; set; }
        public string CurrentLanguage { get; set; }

        private string role;

        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }

        private string tourImageLink;

        public string TourImageLink
        {
            get { return tourImageLink; }
            set
            {
                tourImageLink = value;
                OnPropertyChanged("TourImageLink");
            }
        }

        private string selectedYear;
        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                selectedYear = value;
                OnPropertyChanged("SelectedYear");
                BestTourInfo();
            }
        }

        private ObservableCollection<string> superLanguages;

        public ObservableCollection<string> SuperLanguages
        {
            get { return superLanguages; }
            set
            {
                superLanguages = value;
                OnPropertyChanged("SuperLanguages");
            }
        }

        #region IPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand HomeViewCommand { get; set; }
        public ICommand StatisticsViewCommand { get; set; }
        public ICommand ToSerbianCommand { get; set; }
        public ICommand ToEnglishCommand { get; set; }
        public ICommand QuitJobCommand { get; set; }
        #endregion
        public GuideProfileViewModel(Window window, User loggedInUser)
        {
            this.window = window;
            LoggedInUser = loggedInUser;
            CurrentLanguage = "en-US";

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);
            ToSerbianCommand = new RelayCommand(ToSerbian, CanToSerbian);
            ToEnglishCommand = new RelayCommand(ToEnglish, CanToEnglish);
            QuitJobCommand = new RelayCommand(QuitJob, CanQuitJob);

            Years = new ObservableCollection<string>(tourService.GetAllYears(loggedInUser.Id));
            SelectedYear = "2023";
            TourImageLink = imageService.Get(Tour.ImageId).Url;
            SuperLanguages = new ObservableCollection<string>(reviewService.GetSuperLanguages(LoggedInUser.Id));
            Role = userService.SetRole(loggedInUser, SuperLanguages.Count);
        }

        private bool CanQuitJob()
        {
            return true;
        }

        private void QuitJob()
        {
            userService.QuitJob(LoggedInUser);
            tourService.CancelAllToursByGuide(LoggedInUser.Id);
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

        private bool CanHomeView()
        {
            return true;
        }

        private void HomeView()
        {
            window.Close();
        }

        private bool CanStatisticsView()
        {
            return true;
        }

        private void StatisticsView()
        {
            var statisticsWindow = new StatisticsOfTourView(Tour, LoggedInUser);
            statisticsWindow.Show();
            window.Close();
        }

        private void BestTourInfo()
        {
            Tour = !SelectedYear.Equals("Overall") ? tourService.GetMostVisited(int.Parse(SelectedYear), LoggedInUser) : tourService.GetOverallBest(LoggedInUser);
            OnPropertyChanged("Tour");
        }
    }
}
