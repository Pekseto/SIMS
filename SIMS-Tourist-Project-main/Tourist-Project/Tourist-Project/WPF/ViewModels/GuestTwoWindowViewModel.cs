﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using System.Windows.Input;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestTwoWindowViewModel
    {
        private readonly TourService tourService;
        private readonly LocationService locationService;
        private readonly TourReservationService reservationService;
        private readonly VoucherService voucherService;
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourDTO> Tours { get; set; }
        public TourDTO SelectedTour { get; set; }
        public ObservableCollection<string> Countries { get; set; }

        private string selectedCountry;
        public string SelectedCountry
        {
            get => selectedCountry;
            set
            {
                if(value != selectedCountry)
                {
                    selectedCountry = value;
                    LoadCitiesComboBox(value);
                }
            }
        }

        public ObservableCollection<string> Cities { get; set; }
        public string SelectedCity { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public Voucher SelectedVoucher { get; set; }

        private readonly Regex numberRegex = new Regex("^[0-9]+$");
        private int duration;
        public string Duration
        {
            get { return duration.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    duration = int.Parse(value);
                }
            }
        }

        private int numberOfPeople;
        public string NumberOfPeople
        {
            get { return numberOfPeople.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    numberOfPeople = int.Parse(value);
                }
            }
        }

        private int guestsNumber;
        public string GuestsNumber
        {
            get { return guestsNumber.ToString(); }
            set
            {
                Match match = numberRegex.Match(value);
                if (match.Success)
                {
                    guestsNumber = int.Parse(value);
                }
            }
        }

        private DataGrid toursDataGrid;
        public ICommand SearchCommand { get; set; }
        public ICommand ShowAllCommand { get; set; }
        public ICommand ReserveCommand { get; set; }
        public ICommand VouchersCommand { get; set; }

        public GuestTwoWindowViewModel(User user, DataGrid toursDataGrid)
        {
            LoggedInUser = user;
            this.toursDataGrid = toursDataGrid;

            SearchCommand = new RelayCommand(OnSearchClick);
            ShowAllCommand = new RelayCommand(OnShowAllClick);
            ReserveCommand = new RelayCommand(OnReserveClick);
            VouchersCommand = new RelayCommand(OnVouchersClick);

            tourService = new TourService();
            locationService = new LocationService();
            reservationService = new TourReservationService();
            voucherService = new VoucherService();

            Tours = new ObservableCollection<TourDTO>();
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();
            Vouchers = new ObservableCollection<Voucher>();

            foreach (Tour tour in tourService.GetAll())
            {
                var tourDTO = new TourDTO(tour)
                {
                    SpotsLeft = GetLeftoverSpots(tour),
                    Location = GetLocation(tour)
                };
                Tours.Add(tourDTO);
            }

            foreach (Location location in locationService.GetAll().GroupBy(x => x.Country).Select(y => y.First()))
            {
                Countries.Add(location.Country);
            }

            foreach (Tour tour in tourService.GetAll().GroupBy(x => x.Language).Select(y => y.First()))
            {
                Languages.Add(tour.Language);
            }

            foreach(Voucher voucher in voucherService.GetAll())
            {
                if(voucher.UserId == LoggedInUser.Id && voucher.LastValidDate >= DateTime.Today)
                {
                    Vouchers.Add(voucher);
                }
                else if(voucher.LastValidDate < DateTime.Today)
                {
                    voucherService.Delete(voucher.Id);
                }
            }
        }

        private void OnVouchersClick()
        {
            var VouchersWindow = new VouchersView(LoggedInUser);
            VouchersWindow.Show();
        }

        private void OnReserveClick()
        {
            if (guestsNumber > 0 && SelectedTour != null)
            {
                int tourCapacityLeft = SelectedTour.SpotsLeft;

                if (tourCapacityLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    DisplaySimilarTours(SelectedTour);
                }
                else if (tourCapacityLeft < guestsNumber)
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourCapacityLeft.ToString());
                }
                else
                {
                    SelectedTour.SpotsLeft -= guestsNumber;
                    var tourReservation = new TourReservation(LoggedInUser.Id, SelectedTour.Id, guestsNumber);
                    reservationService.Save(tourReservation);
                    MessageBox.Show("Reservation is successful");

                    if(SelectedVoucher != null)
                    {
                        voucherService.Delete(SelectedVoucher.Id);
                        Vouchers.Remove(SelectedVoucher);
                    }
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number and select a tour\n" +
                                "in order to make a reservation!");
            }
        }

        private void OnShowAllClick()
        {
            toursDataGrid.ItemsSource = Tours;
        }

        private void OnSearchClick()
        {
            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tourDTO in Tours)
            {
                //FILTERING
                if (SelectedCountry != null && tourDTO.Location.Country != SelectedCountry)
                {
                    continue;
                }
                if (SelectedCity != null && tourDTO.Location.City != SelectedCity)
                {
                    continue;
                }
                if (duration != 0 && tourDTO.Duration != duration)
                {
                    continue;
                }
                if (SelectedLanguage != null && tourDTO.Language != SelectedLanguage)
                {
                    continue;
                }
                if (numberOfPeople != 0 && tourDTO.MaxGuestsNumber < numberOfPeople)
                {
                    continue;
                }

                filteredList.Add(tourDTO);
            }
            toursDataGrid.ItemsSource = filteredList;
        }

        private void LoadCitiesComboBox(string country)
        {
            Cities.Clear();
            foreach (Location location in locationService.GetAll())
            {
                if (location.Country == SelectedCountry)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void DisplaySimilarTours(TourDTO selectedTour)
        {
            int locationId = selectedTour.LocationId;
            int selectedTourId = selectedTour.Id;

            var filteredList = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in Tours)
            {
                if (tour.LocationId == locationId && tour.Id != selectedTourId)
                {
                    filteredList.Add(tour);
                }
            }
            toursDataGrid.ItemsSource = filteredList;
        }

        private int GetLeftoverSpots(Tour tour)
        {
            int retVal = tour.MaxGuestsNumber;
            foreach (TourReservation reservation in reservationService.GetAll())
            {
                if (reservation.TourId == tour.Id)
                {
                    retVal -= reservation.GuestsNumber;
                }
            }
            return retVal;
        }
        private Location GetLocation(Tour tour)
        {
            return locationService.GetAll().Find(x => x.Id == tour.LocationId);
        }
    }
}
