using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateTourByLocationViewModel : INotifyPropertyChanged
    {
        #region services
        private readonly LocationService locationService = new();
        private readonly TourService tourService = new();
        private readonly TourPointService tourPointService = new();
        private readonly ImageService imageService = new();
        private readonly TourRequestService requestService = new();
        #endregion

        #region collections

        private ObservableCollection<string> checkpoints;

        public ObservableCollection<string> Checkpoints
        {
            get { return checkpoints; }
            set
            {
                checkpoints = value;
                OnPropertyChanged("Checkpoints");
            }
        }

        private ObservableCollection<string> images;

        public ObservableCollection<string> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged("Images");
            }
        }
        #endregion

        #region command
        public ICommand AddCheckpointCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion

        private readonly TourRequestService tourRequestService = new();

        private Window window;
        private int numberOfPoints = 0;

        public User LoggedInUser;

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

        private Location location;

        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTourByLocationViewModel(User loggedInUser, Window window, Location location)
        {
            LoggedInUser = loggedInUser;
            this.window = window;

            Images = new ObservableCollection<string>();
            Checkpoints = new ObservableCollection<string>();

            ImageForAdd = new();
            PointForAdd = new();
            TourForAdd = new();
            Location = new(location.City, location.Country);

            AddCheckpointCommand = new RelayCommand(AddCheckpoint, CanAddCheckpoint);
            AddImageCommand = new RelayCommand(AddImage, CanAddImage);
            CreateCommand = new RelayCommand(Create, CanCreate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
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
            return numberOfPoints >= 2 && !tourRequestService.IsAlreadyBooked(TourForAdd.StartTime, TourForAdd.Duration);
        }

        private void Create()
        {
            TourForAdd.LocationId = locationService.GetId(Location.City, Location.Country);
            TourForAdd.UserId = LoggedInUser.Id;
            tourService.Save(TourForAdd);

            window.Close();
        }

        private bool CanAddImage()
        {
            return true;
        }

        private void AddImage()
        {
            Images.Add(ImageForAdd.Url);
            imageService.Save(ImageForAdd);
            ImageForAdd = new Image();
        }

        private bool CanAddCheckpoint()
        {
            return true;
        }

        private void AddCheckpoint()
        {
            Checkpoints.Add(PointForAdd.Name);
            PointForAdd.TourId = tourService.NexttId();
            tourPointService.Save(PointForAdd);
            PointForAdd = new TourPoint();
            numberOfPoints++;
        }
    }
}
