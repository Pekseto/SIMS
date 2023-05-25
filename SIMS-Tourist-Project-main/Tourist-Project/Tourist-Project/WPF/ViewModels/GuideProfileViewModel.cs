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

namespace Tourist_Project.WPF.ViewModels
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
        public ICommand SwitchLanguageCommand { get; set; }
        public ICommand QuitJobCommand { get; set; }
        #endregion
        public GuideProfileViewModel(Window window, User loggedInUser)
        {
            this.window = window;
            LoggedInUser = loggedInUser;
            CurrentLanguage = "en-US";

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);
            QuitJobCommand = new RelayCommand(QuitJob, CanQuitJob);

            InitializeYears();
            SelectedYear = "2023";
            TourImageLink = imageService.Get(Tour.ImageId).Url;
            SuperLanguages = new ObservableCollection<string>(reviewService.GetSuperLanguages(LoggedInUser.Id));
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
            var statisticsWindow = new StatisticsOfTourView(Tour);
            statisticsWindow.Show();
            window.Close();
        }

        private void InitializeYears()
        {
            var startYear = DateTime.Now.Year;
            var endYear = startYear - 50;
            Years.Add("Overall");
            for (var year = startYear; year >= endYear; year--)
            {
                Years.Add(year.ToString());
            }
        }

        private void BestTourInfo()
        {
            if (!SelectedYear.Equals("Overall"))
            {
                Tour = tourService.GetMostVisited(Int32.Parse(SelectedYear), LoggedInUser);
            }
            else
            {
                Tour = tourService.GetOverallBest(LoggedInUser);
            }
            OnPropertyChanged("Tour");
        }
    }
}
