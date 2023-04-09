﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TodayToursViewModel
    {
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; } 
        private TourService tourService = new();
        public static bool Live { get; set; } 
        public ICommand CreateCommand { get; set; }
        public ICommand StartTourCommand { get; set; }
        public TodayToursViewModel()
        {
            TodayTours = new ObservableCollection<Tour>(tourService.GetTodaysTours());

            SelectedTour = new();
            Live = false;

            CreateCommand = new RelayCommand(CreateTour, CanCreateTour);
            StartTourCommand = new RelayCommand(StartTour, CanStartTour);
        }

        private bool CanCreateTour()
        {
            return true;
        }
        private void CreateTour()
        {
            var createTourWindow = new CreateTourView();
            createTourWindow.Show();
        }

        private bool CanStartTour()
        {
            if (Live) {
                if (SelectedTour.Status == Status.Begin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (SelectedTour.Status == Status.End || SelectedTour is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
