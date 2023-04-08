using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Tourist_Project.Applications;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateTourViewModel
    {
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();

        private LocationService locationService = new();
        private TourService tourService = new();

        public ICommand CreateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand AddCheckpointCommand { get; set; }
        public event EventHandler RequestClose;

        /*
        public CreateTourViewModel()
        {

            CreateCommand = new RelayCommand(Create, CanCreate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            AddImageCommand = new RelayCommand(AddImage, CanAddImage);
            AddCheckpointCommand = new RelayCommand(AddCheckpoint, CanAddCheckpoint);

            locationService.InitializeCitiesAndCountries();
        }

        private bool CanCancel()
        {
            return true;
        }

        private void Cancel()
        {
            if (CanCancel())
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }
        /*
        private bool CanCreate()
        {
            if(tour.TourPoints.Count() >= 2)
            {
                return true;
            }
            else
            {
                MessageBox.Show("You must enter minimum two checkpoints!");
                return false;
            }
        }

        private void Create()
        {
            tour = new Tour(Name.Text, locationService.GetId(city.Text, country.Text), description.Text, language.Text, Convert.ToInt32(maxGuestsNumber.Text), Convert.ToDateTime(startTime.Text), Convert.ToInt32(duration.Text), image.Id);
            tourService.Save(tour);

            if (tour.StartTime.Date == DateTime.Today.Date)
            {
                TodayToursViewModel.TodayTours.Add(tour);
            }
            this.Close();
        }

        private void CountryDropDownClosed(object sender, EventArgs e, string countryText)
        {
            Cities.Clear();
            foreach (var location in locationService.GetAll())
            {
                if (location.Country.Equals(countryText))
                    Cities.Add(location.City);
            }
        }
        private void CityDropDownClosed(object sender, EventArgs e, string cityText)
        {
            foreach (var location in locationService.GetAll())
            {
                if (location.City.Equals(cityText))
                    Countries.Add(location.Country);
            }
        }

        private bool CanAddImage()
        {
            return true;
        }

        private void AddImage()
        {
            Image image = new Image(url.Text);
            imageService.Save(image);

            if (!string.IsNullOrWhiteSpace(url.Text))
            {
                url.Clear();
            }
        }

        private bool CanAddCheckpoint()
        {
            return true;
        }

        private void AddCheckpoint()
        {
            TourPoint tourPoint = new TourPoint(checkpoint.Text, tourRepository.NextId());
            tourPointRepository.Save(tourPoint);

            if (!string.IsNullOrWhiteSpace(checkpoint.Text))
            {
                tour.TourPoints.Add(tourPoint);
                checkpoint.Clear();
            }
        }
        */
    }
}