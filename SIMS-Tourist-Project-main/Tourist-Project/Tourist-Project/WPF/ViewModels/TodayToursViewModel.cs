using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Application;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TodayToursViewModel
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }
        private TourService tourService;
        public static bool Live { get; set; } = false;
        public ICommand CreateCommand { get; set; }
        public ICommand StartTourCommand { get; set; }
        public TodayToursViewModel()
        {
            TodayTours = new ObservableCollection<Tour>(tourService.GetTodaysTours());

            CreateCommand = new RelayCommand(CreateTour, CanCreateTour);
            StartTourCommand = new RelayCommand(StartTour, CanStartTour);
        }

        private bool CanCreateTour()
        {
            return true;
        }
        private void CreateTour()
        {
            var createTourWindow = new CreateTour();
            createTourWindow.Show();
        }

        private bool CanStartTour()
        {
            if (!Live && !SelectedTour.Guided)
            { 
                Live = true;
                return true;
            }
            else
            {
                MessageBox.Show("Tour is already guided or one is already active");
                return false;
            }
        }
        private void StartTour()
        {
            var tourLiveWindow = new TourLiveView(SelectedTour);
            tourLiveWindow.ShowDialog();

            tourService.Update(SelectedTour);
        }
    }
}
