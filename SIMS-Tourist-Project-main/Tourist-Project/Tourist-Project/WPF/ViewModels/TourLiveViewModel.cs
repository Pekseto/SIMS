using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.View;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourLiveViewModel
    {
        public static ObservableCollection<TourPoint> TourPoints { get; set; }

        public TourPoint SelectedTourPoint { get; set; }
        private Tour selectedTour;
        private Window window;

        private readonly TourPointService tourPointService = new();
        private readonly TourService tourService = new();

        public ICommand EarlyEndCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand OpenTouristListCommand { get; set; }

        public TourLiveViewModel(Tour selectedTour, Window window) 
        {
            this.selectedTour = selectedTour;
            this.window = window;

            TourPoints = new ObservableCollection<TourPoint>(tourPointService.GetAllForTour(selectedTour.Id));
            TourPoints[0].Visited = true;
            tourPointService.Update(TourPoints[0]);

            EarlyEndCommand = new RelayCommand(EarlyEnd, CanEarlyEnd);
            CheckCommand = new RelayCommand(Check, CanCheck);
            OpenTouristListCommand = new RelayCommand(OpenToruistList, CanOpenTouristList);
        }

        private bool CanEarlyEnd()
        {
            return true;
        }
        private void EarlyEnd()
        {
            selectedTour.Status = Status.End;
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
            tourPointService.UpdateCollection(SelectedTourPoint, selectedTour);
            if (tourPointService.EndTour())
            {
                selectedTour.Status = Status.End;
                tourService.Update(selectedTour);
                window.Close();
            }
        }

        private bool CanOpenTouristList()
        {
            return true;
        }

        private void OpenToruistList()
        {
            var touristListWindow = new TouristListView(SelectedTourPoint);
            touristListWindow.Show();
        }
    }
}
