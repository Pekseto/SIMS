using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.DTO;
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Repository;
using Image = Tourist_Project.Model.Image;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for OwnerShowWindow.xaml
    /// </summary>
    public partial class OwnerShowWindow : Window
    {
        public static ObservableCollection<Accommodation> accommodations { get; set; }
        public static ObservableCollection<Location> locations { get; set; }
        public static ObservableCollection<Image> images { get; set; }
        public static ObservableCollection<AccommodationDTO> accommodationDTOs { get; set; }
        public Accommodation selectedAccommodation { get; set; }
        public AccommodationDTO selectedAccommodationDTO { get; set; }
        //public User LoggedInUser { get; set; }
        private readonly AccommodationRepository accommodationRepository = new();
        private readonly LocationRepository locationRepository = new();
        private readonly ImageRepository imageRepository = new();
        private readonly AccommodationDTORepository accommodationDTORepository = new();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OwnerShowWindow()
        {
            InitializeComponent();
            DataContext = this;
            //LoggedInUser = user;
            accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());
            locations = new ObservableCollection<Location>(locationRepository.GetAll());
            images = new ObservableCollection<Image>(imageRepository.GetAll());
            accommodationDTOs = new ObservableCollection<AccommodationDTO>(accommodationDTORepository.createDTOs(accommodations, locations, images));
        }

        private void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            var createWindow = new AccommodationForm();
            createWindow.Show();
        }
        private void ShowViewAccommodationForm(object sender, RoutedEventArgs e)
        {
            selectedAccommodation = accommodationRepository.GetById(selectedAccommodationDTO.AccommodationId);
            var viewWindow = new AccommodationForm(selectedAccommodation);
            viewWindow.Show();
        }
        private void ShowUpdateAccommodationForm(object sender, RoutedEventArgs e)
        {
            selectedAccommodation = accommodationRepository.GetById(selectedAccommodationDTO.AccommodationId);
            var updateWindow = new AccommodationForm(selectedAccommodation, selectedAccommodationDTO);
            updateWindow.Show();
            Close();
        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (selectedAccommodationDTO != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete comment",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    accommodationRepository.Delete(selectedAccommodationDTO.AccommodationId);
                    accommodations.Remove(selectedAccommodation);
                    accommodationDTOs.Remove(selectedAccommodationDTO);
                }
            }
        }
    }
}
