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
            selectedAccommodation.Location = locationRepository.GetLocation(selectedAccommodation.LocationId);
            LoadImages(selectedAccommodation);
            Name.Text = selectedAccommodation.Name;
            Country.Text = selectedAccommodation.Location.Country;
            City.Text = selectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = imageRepository.GetImage(selectedAccommodation.ImageId).Url;
            Title.Content = selectedAccommodation.Name;
        }

        private void LoadImages(Accommodation selectedAccommodation)
        {
            foreach (var imageId in selectedAccommodation.ImageIdes)
            {
                Images.Add(imageRepository.GetImage(imageId).Url);
            }
        }

        private void EnableEditing()
        {
            Name.IsEnabled = false;
            Country.IsEnabled = false;
            City.IsEnabled = false;
            Type.IsEnabled = false;
            MaxNumGuests.IsEnabled = false;
            MinStayingDays.IsEnabled = false;
            DaysBeforeCancel.IsEnabled = false;
            Url.IsEnabled = false;
        }
        public void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
