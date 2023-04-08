using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateAccommodationViewModel : INotifyPropertyChanged
    {
        #region ToCreate
        private Accommodation accommodationToCreate;
        public Accommodation AccommodationToCreate
        {
            get => accommodationToCreate;
            set
            {
                accommodationToCreate = value;
                OnPropertyChanged("AccommodationToCreate");
            }
        }
        private Location locationToCreate;
        public Location LocationToCreate
        {
            get => locationToCreate;
            set
            {
                locationToCreate = value;
                OnPropertyChanged("LocationToCreate");
            }
        }
        private Image imageToCreate;
        public Image ImageToCreate
        {
            get => imageToCreate;
            set
            {
                imageToCreate = value;
                OnPropertyChanged("ImageToCreate");
            }
        }
        #endregion

        private readonly ImageService imageService = new();
        private readonly LocationService locationService = new();
        private readonly AccommodationService accommodationService = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<Image> Images { get; set; } = new();
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ICommand ConfirmCommand { get; set; }
        public static ICommand CancelCommand { get; set; }
        public Window Window;
        
        public CreateAccommodationViewModel(Window window)
        {
            Locations = new ObservableCollection<Location>(locationService.GetAll());
            Images = new ObservableCollection<Image>(imageService.GetAll());
            LocationToCreate = new Location();
            AccommodationToCreate = new Accommodation();
            ImageToCreate = new Image();
            InitializeCitiesAndCountries();
            ConfirmCommand = new RelayCommand(Create, CanCreate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            Window = window;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private static void InitializeCitiesAndCountries()
        {
            foreach (var location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                    Countries.Add(location.Country);
            }
        }

        public void Create()
        {

            AccommodationToCreate.LocationId = locationService.GetId(LocationToCreate.City, LocationToCreate.Country);
            accommodationService.Create(AccommodationToCreate);
        }

        public static bool CanCreate()
        {
            return true;
        }
        public void Cancel()
        {
            Window.Close();
        }

        public static bool CanCancel()
        {
            return true;
        }

    }

}