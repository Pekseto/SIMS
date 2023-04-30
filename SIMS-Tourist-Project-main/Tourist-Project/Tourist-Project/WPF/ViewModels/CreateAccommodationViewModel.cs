using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Owner;

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
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ICommand ConfirmCommand { get; set; }
        public static ICommand CancelCommand { get; set; }
        public CreateAccommodation Window;
        
        public CreateAccommodationViewModel(CreateAccommodation window)
        {
            Locations = new ObservableCollection<Location>(locationService.GetAll());
            LocationToCreate = new Location();
            AccommodationToCreate = new Accommodation();
            ImageToCreate = new Image();
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(locationService.GetAllCities());
            ConfirmCommand = new RelayCommand(Create, CanCreate);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            Window = window;
            Window.Country.DropDownClosed += CountryDropDownClosed;
            Window.City.DropDownClosed += CityDropDownClosed;
        }

        public void Create()
        {
            AccommodationToCreate.ImageIdsCsv = imageService.FormIdesString(ImageToCreate.Url);
            AccommodationToCreate.LocationId = locationService.GetId(LocationToCreate.City, LocationToCreate.Country);
            accommodationService.Create(AccommodationToCreate);
            Window.Close();
        }
        #region PropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Commands

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
        public void CountryDropDownClosed(object sender, EventArgs e)
        {
            Cities.Clear();
            foreach (var location in Locations)
            {
                if (location.Country.Equals(Window.Country.Text))
                    Cities.Add(location.City);
            }
        }
        public void CityDropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in Locations)
            {
                if (location.City.Equals(Window.City.Text))
                    Window.Country.Text = location.Country;
            }
        } 
        #endregion
    }

}