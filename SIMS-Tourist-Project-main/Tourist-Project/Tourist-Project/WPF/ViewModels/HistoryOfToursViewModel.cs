using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class HistoryOfToursViewModel
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        private TourService tourService = new();
        public Tour SelectedTour { get; set; }
        private Window window;

        public ICommand HomeViewCommand { get; set; }
        public ICommand FutureViewCommand { get; set; }
        public ICommand StatisticsViewCommand { get; set; }
        public ICommand ReviewViewCommand { get; set; }
        public HistoryOfToursViewModel(Window window) 
        {
            this.window = window;
            SelectedTour = null;
            Tours = new ObservableCollection<Tour>(tourService.GetPastTours());

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            FutureViewCommand = new RelayCommand(FutureView, CanFutureView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);
            ReviewViewCommand = new RelayCommand(ReviewView, CanReviewView);
        }

        private bool CanHomeView()
        {
            return true;
        }

        private void HomeView()
        {
            var homeWindow = new TodayToursView();
            homeWindow.Show();
            window.Close();
        }

        private bool CanFutureView()
        {
            return true;
        }

        private void FutureView()
        {
            var futureWindow = new FutureToursView();
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

        private void StatisticsView()
        {
            var statisticsWindow = new StatisticsOfTourView(SelectedTour);
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

        private void ReviewView()
        {
            var reviewWindow = new TourReviewsGuideView(SelectedTour);
            reviewWindow.Show();
        }
    }
}
