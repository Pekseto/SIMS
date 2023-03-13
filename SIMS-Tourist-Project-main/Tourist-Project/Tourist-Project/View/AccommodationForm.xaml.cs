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
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for AddAccommodation.xaml
    /// </summary>
    public partial class AccommodationForm : Window
    {
        private readonly ImageRepository imageRepository;
        private readonly AccommodationRepository accommodationRepository;
        public AccommodationForm()
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            Title = "Create new accommodation";
        }

        public AccommodationForm(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            EnableEditing();
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
        public AccommodationForm(Accommodation selectedAccommodation, OwnerShowWindow ownerShowWindow)
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            Name.Text = selectedAccommodation.Name;
            Country.Text = selectedAccommodation.Location.Country;
            City.Text = selectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = selectedAccommodation.Image.Url;
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
            //Accommodation newAccommodation = new Accommodation(Name.Text, location, Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(DaysBeforeCancel.Text), image);
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
