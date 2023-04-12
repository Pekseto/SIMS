using System.Collections.ObjectModel;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class OwnerMainWindowViewModel
    {
        public static ObservableCollection<Accommodation> accommodations { get; set; }
        private static AccommodationService accommodationService = new();
        private readonly LocationService locationService = new();
        private readonly ImageService imageService = new();
        public static Accommodation SelectedAccommodation { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public OwnerMainWindowViewModel()
        {
            CreateCommand = new RelayCommand(Create, CanCreate);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
            foreach (var accommodation in accommodations)
            {
                accommodation.Location = locationService.Get(accommodation.LocationId);
                accommodation.ImageUrl = imageService.Get(accommodation.ImageId).Url;
            }
        }

        public static void Create()
        {
            var createWindow = new CreateAccommodation();
            createWindow.ShowDialog();
        }

        public static bool CanCreate()
        {
            return true;
        }

        public static void Update()
        {
            var updateWindow = new UpdateAccommodation(SelectedAccommodation);
            updateWindow.ShowDialog();
            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());
        }

        public static bool CanUpdate()
        {
            return true;
        }
    }

}