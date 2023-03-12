using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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
using Tourist_Project.Controller;
using Tourist_Project.Model;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for AddAccommodation.xaml
    /// </summary>
    public partial class AccommodationForm : Window
    {
        public static AccommodationController accommodationController;
        public static LocationController locationController;
        public static ImageController imageController;
        public AccommodationForm(AccommodationController controller)
        {
            InitializeComponent();
            DataContext = this;
            accommodationController = controller;
            locationController = new LocationController();
            imageController = new ImageController();
            Title = "Create new accommodation";
        }

        public AccommodationForm(AccommodationController controller, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            EnableEditing();
            accommodationController = controller;
            locationController = new LocationController();
            imageController = new ImageController();
            Name.Text = selectedAccommodation.Name;
            Country.Text = selectedAccommodation.Location.Country;
            City.Text = selectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = selectedAccommodation.Image.Url;
            btnSave.Visibility = Visibility.Collapsed;
            Title = "View accommodation";
        }
        public AccommodationForm(Accommodation selectedAccommodation, AccommodationController controller)
        {
            InitializeComponent();
            DataContext = this;
            accommodationController = controller;
            locationController = new LocationController();
            imageController = new ImageController();
            Name.Text = selectedAccommodation.Name;
            Country.Text = selectedAccommodation.Location.Country;
            City.Text = selectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = selectedAccommodation.Image.Url;
            btnSave.Visibility = Visibility.Collapsed;
            Title = "Update accommodation";
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
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            Location location = new Location(locationController.GetId(City.Text, Country.Text), City.Text, Country.Text);
            Model.Image image = new Model.Image(imageController.GetId(Url.Text), Url.Text);
            Accommodation newAccommodation = new Accommodation(Name.Text, location, Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(DaysBeforeCancel.Text), image);
            accommodationController.Add(newAccommodation);
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
