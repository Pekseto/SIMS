using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.Guide;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourLiveViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<TourPoint> TourPoints { get; set; }

        public TourPoint SelectedTourPoint { get; set; }
        public Tour SelectedTour { get; set; }
        public string CurrentLanguage { get; set; }

        private Window window;
        private readonly TourPointService tourPointService = new();
        private readonly TourService tourService = new();

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

        #region Command
        public ICommand EarlyEndCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand OpenTouristListCommand { get; set; }
        public ICommand HomePageCommand { get; set; }
        public ICommand SwitchLanguageCommand { get; set; }
        #endregion

        public TourLiveViewModel(Tour selectedTour, Window window) 
        {
            this.SelectedTour = selectedTour;
            this.window = window;

            TourPoints = new ObservableCollection<TourPoint>(tourPointService.GetAllForTour(selectedTour.Id));
            TourPoints[0].Visited = true;
            tourPointService.Update(TourPoints[0]);

            startClock();
            CurrentLanguage = "en - US";
            SelectedTourPoint = null;


            EarlyEndCommand = new RelayCommand(EarlyEnd, CanEarlyEnd);
            CheckCommand = new RelayCommand(Check, CanCheck);
            OpenTouristListCommand = new RelayCommand(OpenToruistList, CanOpenTouristList);
            HomePageCommand = new RelayCommand(HomeView, CanHomeView);
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


        private bool CanHomeView()
        {
            return true;
        }

        private void HomeView()
        {
            window.Close();
        }

        private bool CanEarlyEnd()
        {
            return true;
        }
        private void EarlyEnd()
        {
            SelectedTour.Status = Status.End;
            TodayToursViewModel.Live = false;
            window.Close();
        }

        private bool CanCheck()
        {
            if(SelectedTourPoint != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Check()
        {
            SelectedTourPoint.Visited = true;
            tourPointService.UpdateCollection(SelectedTourPoint, SelectedTour);
            if (tourPointService.EndTour())
            {
                SelectedTour.Status = Status.End;
                tourService.Update(SelectedTour);
                window.Close();
            }
        }

        private bool CanOpenTouristList()
        {
            return SelectedTourPoint is not null;
        }

        private void OpenToruistList()
        {
            var touristListWindow = new TouristListView(SelectedTourPoint, SelectedTour);
            touristListWindow.Show();
            window.Close();
        }
    }
}
