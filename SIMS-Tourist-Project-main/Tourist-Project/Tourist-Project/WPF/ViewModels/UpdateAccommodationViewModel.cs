using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private readonly ImageService imageService = new();
        private readonly LocationService locationService = new();
        private readonly AccommodationService accommodationService = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ICommand ConfirmCommand { get; set; }
        public Window window;

        public UpdateAccommodationViewModel(Window window, Accommodation accommodation)
        {
            Accommodation = accommodation;
            Image = new Image();
            Locations = new ObservableCollection<Location>(locationService.GetAll());
            InitializeCitiesAndCountries();
            ConfirmCommand = new RelayCommand(Update, CanUpdate);
            this.window = window;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
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
    }
}