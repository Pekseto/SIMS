using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class StatisticsOfTourViewModel
    {
        private readonly Window window;

        private readonly LocationService locationService = new();
        private readonly TourReservationService tourReservationService = new();

        public Tour Tour { get; set; }
        public Location Location { get; set; }

        #region StatisticsData
        public int TouristsNumberYounger { get; set; } = 0;
        public int TouristsNumberBetween { get; set; } = 0;
        public int TouristsNumberOlder { get; set; } = 0;
        public double WithVoucher { get; set; }
        public double WithoutVoucher { get; set; }
        #endregion
        public ICommand BackCommand { get; set; }

        public StatisticsOfTourViewModel(Window window, Tour tour)
        {
            this.window = window;
            Tour = tour;
            Location = locationService.Get(Tour.LocationId);

            BackCommand = new RelayCommand(Back, CanBack);

            TourAgeStatistics();
            TourVoucherStatistics();
        }

        private bool CanBack()
        {
            return true;
        }

        private void Back()
        {
            window.Close();
        }

        private void TourAgeStatistics()
        {
            TouristsNumberYounger = tourReservationService.CountingTourists(Tour.Id)[0];
            TouristsNumberBetween = tourReservationService.CountingTourists(Tour.Id)[1];
            TouristsNumberOlder = tourReservationService.CountingTourists(Tour.Id)[2];
        }

        private void TourVoucherStatistics()
        {
            WithVoucher = tourReservationService.WithVoucherPercent(Tour.Id);
            WithoutVoucher = tourReservationService.WithOutVoucherPercent(Tour.Id);
        }
    }
}
