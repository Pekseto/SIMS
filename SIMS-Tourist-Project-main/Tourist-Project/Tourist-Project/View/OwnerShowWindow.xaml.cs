using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Views;
using Image = Tourist_Project.Domain.Models.Image;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for OwnerShowWindow.xaml
    /// </summary>
    public partial class OwnerShowWindow : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; } = new();
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<Image> Images { get; set; } = new();
        public static ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; } = new();
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationDTO SelectedAccommodationDto { get; set; }
        private readonly AccommodationRepository accommodationRepository = new();
        private readonly LocationRepository locationRepository = new();
        private readonly ImageRepository imageRepository = new();
        private readonly AccommodationDtoRepository accommodationDTORepository = new();

        public OwnerShowWindow()
        {
            InitializeComponent();
            DataContext = this;
            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            AccommodationDTOs = new ObservableCollection<AccommodationDTO>(accommodationDTORepository.LoadAll(Accommodations, Locations, Images));
        }

        private void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            var createWindow = new AccommodationForm();
            createWindow.Show();
        }
        private void ShowViewAccommodationForm(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodationDto != null)
            {
                SelectedAccommodation = accommodationRepository.GetById(SelectedAccommodationDto.AccommodationId);
                var viewWindow = new AccommodationViewWindow(SelectedAccommodation);
                viewWindow.Show();
            }
            else
            {
                MessageBox.Show("You have to select accommodation!");
            }
        }
        private void ShowUpdateAccommodationForm(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodationDto != null)
            {
                SelectedAccommodation = accommodationRepository.GetById(SelectedAccommodationDto.AccommodationId);
                var updateWindow = new AccommodationForm(SelectedAccommodation, SelectedAccommodationDto);
                updateWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You have to select accommodation!");
            }
        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodationDto != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Remove accommodation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Remove();
                }
            }
            else
            {
                MessageBox.Show("You have to select accommodation!");
            }
        }
        private void Remove()
        {
            accommodationRepository.Delete(SelectedAccommodationDto.AccommodationId);
            Accommodations.Remove(SelectedAccommodation);
            AccommodationDTOs.Remove(SelectedAccommodationDto);
        }
    }
}
