using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class FutureToursViewModel : INotifyPropertyChanged
    {
        private Window window;
        private Tour tour;

        public string CurrentLanguage;

        public static ObservableCollection<Tour> FutureTours { get; set; }
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

        public Tour SelectedTour
        {
            get => tour;
            set
            {
                tour = value;
                OnPropertyChanged("SelectedTour");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private readonly TourService tourService = new();
        private readonly TourVoucherService voucherService = new();

        #region Command
        public ICommand CancelTourCommand { get; set; }
        public ICommand HomePageCommand { get; set; }
        public ICommand ProfileViewCommand { get; set; }
        public ICommand SwitchLanguageCommand { get; set; }
        public ICommand RequestsViewCommand { get; set; }
        #endregion

        public FutureToursViewModel(Window window)
        {
            FutureTours = new ObservableCollection<Tour>(tourService.GetFutureTours());

            this.window = window;
            CurrentLanguage = "en-US";
            startClock();

            HomePageCommand = new RelayCommand(HomePage, CanHomePage);
            CancelTourCommand = new RelayCommand(CancelTour, CanCancelTour);
            ProfileViewCommand = new RelayCommand(ProfileView, CanProfileView);
            RequestsViewCommand = new RelayCommand(RequestsView, CanRequestsView);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage, CanSwitchLanguage);
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

        private void RequestsView()
        {
            var requestsWindow = new RequestsGuideView();
            requestsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            requestsWindow.Show();
            window.Close();
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

        private bool CanHomePage()
        {
            return true;
        }
        private void HomePage()
        {
            var todayToursView = new TodayToursView();
            todayToursView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            todayToursView.Show();
            window.Close();
        }

        private bool CanCancelTour()
        {
            if (SelectedTour is null)
            {
                return false;
            }
            else
            {
                if ((SelectedTour.StartTime - DateTime.Now).TotalHours >= 48)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void CancelTour()
        {
            SelectedTour.Status = Status.Cancel;
            voucherService.VouchersDistribution(SelectedTour.Id);

            UpdateData();
        }

        private bool CanProfileView()
        {
            return true;
        }
        private void ProfileView()
        {
            var profileView = new GuideProfileView();
            profileView.Owner = window;
            profileView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            profileView.Show();
        }

        private void UpdateData()
        {
            tourService.Update(SelectedTour);
            FutureTours.Clear();
            foreach (var tour in tourService.GetFutureTours())
            {
                FutureTours.Add(tour);
            }
        }
    }
}
