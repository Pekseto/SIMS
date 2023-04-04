using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Model;
using Tourist_Project.Repository;
using Tourist_Project.View;
using Image = Tourist_Project.Domain.Models.Image;

namespace Tourist_Project
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
        public AccommodationDTO SelectedAccommodationDTO { get; set; }
        private readonly AccommodationRepository accommodationRepository = new();
        private readonly LocationRepository locationRepository = new();
        private readonly ImageRepository imageRepository = new();
        private readonly AccommodationDTORepository accommodationDTORepository = new();

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
            if (SelectedAccommodationDTO != null)
            {
                SelectedAccommodation = accommodationRepository.GetById(SelectedAccommodationDTO.AccommodationId);
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
            if (SelectedAccommodationDTO != null)
            {
                SelectedAccommodation = accommodationRepository.GetById(SelectedAccommodationDTO.AccommodationId);
                var updateWindow = new AccommodationForm(SelectedAccommodation, SelectedAccommodationDTO);
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
            if (SelectedAccommodationDTO != null)
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
            accommodationRepository.Delete(SelectedAccommodationDTO.AccommodationId);
            Accommodations.Remove(SelectedAccommodation);
            AccommodationDTOs.Remove(SelectedAccommodationDTO);
        }
    }
}
