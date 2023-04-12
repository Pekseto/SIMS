using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class UpdateAccommodationViewModel : INotifyPropertyChanged
    {
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

        private readonly ImageService imageService = new();
        private readonly LocationService locationService = new();
        private readonly AccommodationService accommodationService = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ICommand ConfirmCommand { get; set; }
        public Window Window;

        public UpdateAccommodationViewModel(Window window, Accommodation accommodation)
        {
            Accommodation = accommodation;
            Image = new Image();
            Location = new Location();
            Locations = new ObservableCollection<Location>(locationService.GetAll());
            InitializeCitiesAndCountries();
            ConfirmCommand = new RelayCommand(Update, CanUpdate);
            Window = window;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            Accommodation.ImageIds = CreateImage();
            Accommodation.ImageIdsCSV = FormIdesString(Accommodation.ImageIds);
            Accommodation.LocationId = locationService.GetId(Location.City, Location.Country);
            accommodationService.Update(accommodation);
            Window.Close();
        }

        public static bool CanUpdate()
        {
            return true;
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
        private List<int> CreateImage()
        {
            List<int> ides = new();
            if (!Image.Url.Contains(","))
            {
                Image newImage = new(Image.Url);
                var savedImage = imageService.Create(newImage);
                ides.Add(savedImage.Id);
            }
            else
            {
                foreach (var url in Image.Url.Split(","))
                {
                    Image newImage = new(url);
                    var savedImage = imageService.Create(newImage);
                    ides.Add(savedImage.Id);
                }
            }

            return ides;
        }
        private static string? FormIdesString(List<int> ids)
        {
            if (ids.Count <= 0) return null;
            var ides = ids.Aggregate(string.Empty, (current, imageId) => current + (imageId + ","));
            ides = ides.Remove(ides.Length - 1);
            return ides;
        }
    }
}