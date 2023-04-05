using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for AccommodationViewWindow.xaml
    /// </summary>
    public partial class AccommodationViewWindow : Window
    {
        public static ObservableCollection<string> Images { get; set; } = new();
        private readonly ImageRepository imageRepository = new();
        private readonly LocationRepository locationRepository = new();
        public AccommodationViewWindow(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            EnableEditing();
            InitializeFields(selectedAccommodation);
        }

        private void InitializeFields(Accommodation selectedAccommodation)
        {
            selectedAccommodation.Location = locationRepository.GetById(selectedAccommodation.LocationId);
            LoadImages(selectedAccommodation);
            Name.Text = selectedAccommodation.Name;
            Country.Text = selectedAccommodation.Location.Country;
            City.Text = selectedAccommodation.Location.City;
            type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            CancellationThreshold.Text = selectedAccommodation.CancellationThreshold.ToString();
            Url.Text = imageRepository.GetById(selectedAccommodation.ImageId).Url;
            Title.Content = selectedAccommodation.Name;
        }

        private void LoadImages(Accommodation selectedAccommodation)
        {
            foreach (var imageId in selectedAccommodation.ImageIds)
            {
                Images.Add(imageRepository.GetById(imageId).Url);
            }
        }

        private void EnableEditing()
        {
            Name.IsEnabled = false;
            Country.IsEnabled = false;
            City.IsEnabled = false;
            type.IsEnabled = false;
            MaxNumGuests.IsEnabled = false;
            MinStayingDays.IsEnabled = false;
            CancellationThreshold.IsEnabled = false;
            Url.IsEnabled = false;
        }
        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
