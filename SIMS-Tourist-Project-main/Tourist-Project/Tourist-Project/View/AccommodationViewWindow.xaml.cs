using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
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
            name.Text = selectedAccommodation.Name;
            country.Text = selectedAccommodation.Location.Country;
            city.Text = selectedAccommodation.Location.City;
            type.Text = selectedAccommodation.Type.ToString();
            maxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            minStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            cancellationThreshold.Text = selectedAccommodation.CancellationThreshold.ToString();
            url.Text = imageRepository.Get(selectedAccommodation.ImageId).Url;
            title.Content = selectedAccommodation.Name;
        }

        private void LoadImages(Accommodation selectedAccommodation)
        {
            foreach (var imageId in selectedAccommodation.ImageIds)
            {
                Images.Add(imageRepository.Get(imageId).Url);
            }
        }

        private void EnableEditing()
        {
            name.IsEnabled = false;
            country.IsEnabled = false;
            city.IsEnabled = false;
            type.IsEnabled = false;
            maxNumGuests.IsEnabled = false;
            minStayingDays.IsEnabled = false;
            cancellationThreshold.IsEnabled = false;
            url.IsEnabled = false;
        }
        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
