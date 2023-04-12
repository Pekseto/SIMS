using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        #region ObservableCollection
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        #endregion

        #region Service
        private LocationService locationService = new();
        private TourService tourService = new();
        private TourPointService tourPointService = new();
        private ImageService imageService = new();
        #endregion
        private int numberOfPoints = 0;
        private Window window;
        private User user;

        #region Command
        public ICommand CreateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand AddCheckpointCommand { get; set; }
        #endregion
        
        #region ObjectForAdd
        private Tour tourForAdd;
        public Tour TourForAdd

        {
            get { return tourForAdd; }
            set
            {
                tourForAdd = value;
                OnPropertyChanged("TourForAdd");
            }
        }
        private Image imageForAdd;
        public Image ImageForAdd
        {
            get { return imageForAdd; }
            set
            {
                imageForAdd = value;
                OnPropertyChanged("ImageForAdd");
            }
        }
        private TourPoint pointForAdd;
        public TourPoint PointForAdd
        {
            get { return pointForAdd; }
            set
            {
                pointForAdd = value;
                OnPropertyChanged("PointForAdd");
            }
        }
        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("PointForAdd");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTourViewModel(Window window)
        {
            this.window = window;
            Cities = new ObservableCollection<string>(locationService.GetAllCities());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());

            ImageForAdd = new();
            PointForAdd = new();
            TourForAdd = new();
            Location = new();

            CreateCommand = new RelayCommand(Create, CanCreate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            AddImageCommand = new RelayCommand(AddImage, CanAddImage);
            AddCheckpointCommand = new RelayCommand(AddCheckpoint, CanAddCheckpoint);
        }

        private bool CanCancel()
        {
            return true;
        }

        private void Cancel()
        {
            window.Close();
        }
        
        private bool CanCreate()
        {
            
            if(numberOfPoints >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Create()
        {
            TourForAdd.LocationId = locationService.GetId(Location.City, Location.Country);
            TourForAdd.UserId = MainWindow.LoggedInUser.Id;
            tourService.Save(TourForAdd);

            if (TourForAdd.StartTime.Date == DateTime.Today.Date)
            {
                TodayToursViewModel.TodayTours.Add(TourForAdd);
            }

            window.Close();
        }

        private void CountryDropDownClosed(object sender, EventArgs e)
        {
            Cities.Clear();
            foreach (var location in locationService.GetAll())
            {
                if (location.Country.Equals(Location.Country))
                    Cities.Add(location.City);
            }
        }
        private void CityDropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in locationService.GetAll())
            {
                if (location.City.Equals(Location.City))
                    Countries.Add(location.Country);
            }
        }

        private bool CanAddImage()
        {
            return true;
        }

        private void AddImage()
        {
            imageService.Save(ImageForAdd);
            ImageForAdd = new Image();
        }

        private bool CanAddCheckpoint()
        {
            return true;
        }

        private void AddCheckpoint()
        {
            PointForAdd.TourId = tourService.NexttId();
            tourPointService.Save(PointForAdd);
            PointForAdd = new TourPoint();
            numberOfPoints++;
        }
        
    }
}