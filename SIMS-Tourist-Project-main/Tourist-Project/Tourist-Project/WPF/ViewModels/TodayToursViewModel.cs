using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TodayToursViewModel
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; } 
        private readonly TourService tourService = new();
        private readonly Window window;
        public static bool Live { get; set; } 
        public ICommand CreateCommand { get; set; }
        public ICommand StartTourCommand { get; set; }
        public ICommand FutureToursCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand RequestsCommand { get; set; }
        public TodayToursViewModel(Window window)
        {
            TodayTours = new ObservableCollection<Tour>(tourService.GetTodaysTours());

            SelectedTour = new Tour();
            this.window = window;
            Live = false;

            CreateCommand = new RelayCommand(CreateTour, CanCreateTour);
            StartTourCommand = new RelayCommand(StartTour, CanStartTour);
            FutureToursCommand = new RelayCommand(FutureTours, CanFutureTours);
            HistoryCommand = new RelayCommand(History, CanHistory);
            RequestsCommand = new RelayCommand(Requests, CanRequests);
            this.window = window;
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
