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
using Image = Tourist_Project.Model.Image;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for AddAccommodation.xaml
    /// </summary>
    public partial class AccommodationForm : Window
    {
        private readonly ImageRepository imageRepository;
        private readonly AccommodationRepository accommodationRepository;
        private readonly LocationRepository locationRepository;
        public User LoggedInUser { get; set; }
        public Accommodation SelectedAccommodation;
        public AccommodationForm()
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            locationRepository = new LocationRepository();
            Title = "Create new accommodation";
        }

        public AccommodationForm(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            locationRepository = new LocationRepository();
            EnableEditing();
            SelectedAccommodation = selectedAccommodation;
            btnSave.Visibility = Visibility.Collapsed;
            Title = "View accommodation";
        }
        public AccommodationForm(Accommodation selectedAccommodation, OwnerShowWindow ownerShowWindow)
        {
            InitializeComponent();
            DataContext = this;
            imageRepository = new ImageRepository();
            accommodationRepository = new AccommodationRepository();
            locationRepository = new LocationRepository();
            SelectedAccommodation = selectedAccommodation;
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
            if(SelectedAccommodation != null)
            {
                SelectedAccommodation.Name = Name.Text;
                SelectedAccommodation.LocationId = GetId();
                SelectedAccommodation.Type = Enum.Parse<AccommodationType>(Type.Text);
                SelectedAccommodation.MaxGuestNum = int.Parse(MaxNumGuests.Text);
                SelectedAccommodation.MinStayingDays = int.Parse(MinStayingDays.Text);
                SelectedAccommodation.DaysBeforeCancel = int.Parse(DaysBeforeCancel.Text);
                SelectedAccommodation.ImageId =  CreateImage();
                Accommodation updatedAccommodation = accommodationRepository.Update(SelectedAccommodation);
                if(updatedAccommodation != null)
                {
                    int index = OwnerShowWindow.accommodations.IndexOf(SelectedAccommodation);
                    OwnerShowWindow.accommodations.Remove(SelectedAccommodation);
                    OwnerShowWindow.accommodations.Insert(index, updatedAccommodation);
                }
            }
            else
            {
                Accommodation newAccommodation = new Accommodation(Name.Text, GetId(), Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(DaysBeforeCancel.Text), CreateImage());
                Accommodation savedAccommodation = accommodationRepository.Save(newAccommodation);
                OwnerShowWindow.accommodations.Add(savedAccommodation);
            }
            Close();
        }

        private int GetId()
        {
            return locationRepository.GetId(City.Text, Country.Text);
        }

        private int CreateImage()
        {
            Image newImage = new Image(Url.Text);
            Image savedImage = imageRepository.Save(newImage);
            return savedImage.Id;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
