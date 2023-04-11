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
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class FutureToursViewModel
    {
        private Window window;

        public static ObservableCollection<Tour> FutureTours { get; set; }

        public string CurrentTime { get; set; } = DateTime.Now.ToString();

        public Tour SelectedTour { get; set; }
        private TourService tourService = new();
        private TourVoucherService voucherService = new();
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
            tourService.Update(SelectedTour);

            voucherService.VouchersDistribution(SelectedTour.Id);
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
    }
}
