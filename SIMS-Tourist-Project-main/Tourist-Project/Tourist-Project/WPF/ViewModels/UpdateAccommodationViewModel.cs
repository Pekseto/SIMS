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
    public class UpdateAccommodationViewModel : INotifyPropertyChanged
    {
        #region UpdateProperties
        private Accommodation accommodation;

        public Accommodation Accommodation
        {
            get => accommodation;
            set
            {
                accommodation = value;
                OnPropertyChanged("Accommodation");
            }
        }

        private Image image;

        public Image Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        private Location location;

        public Location Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChanged("Location");
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
        public UpdateAccommodation Window;

        public UpdateAccommodationViewModel(UpdateAccommodation window, Accommodation accommodation)
        {
            Accommodation = accommodation;
            Image = new Image();
            Location = new Location();
            Locations = new ObservableCollection<Location>(locationService.GetAll());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(locationService.GetAllCities());
            ConfirmCommand = new RelayCommand(Update, CanUpdate);
            Window = window;
            Window.Country.DropDownClosed += CountryDropDownClosed;
            Window.City.DropDownClosed += CityDropDownClosed;
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Commands
        public void Update()
        {
            Accommodation.ImageIdsCsv = imageService.FormIdesString(Image.Url);
            Accommodation.LocationId = locationService.GetId(Location.City, Location.Country);
            accommodationService.Update(accommodation);
            Window.Close();
        }

        public static bool CanUpdate()
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