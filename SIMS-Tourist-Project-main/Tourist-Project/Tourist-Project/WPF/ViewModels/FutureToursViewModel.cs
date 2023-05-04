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

        public static ObservableCollection<Tour> FutureTours { get; set; }


        private Tour tour;

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
        public ICommand CancelTourCommand { get; set; }
        public ICommand HomePageCommand { get; set; }
        public ICommand ProfileViewCommand { get; set; }
        public FutureToursViewModel(Window window)
        {
            this.window = window;

            FutureTours = new ObservableCollection<Tour>(tourService.GetFutureTours());

            HomePageCommand = new RelayCommand(HomePage, CanHomePage);
            CancelTourCommand = new RelayCommand(CancelTour, CanCancelTour);
            ProfileViewCommand = new RelayCommand(ProfileView, CanProfileView);

        }

        private bool CanHomePage()
        {
            return true;
        }
        private void HomePage()
        {
            var todayToursView = new TodayToursView();
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
            profileView.Show();
            window.Close();
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
