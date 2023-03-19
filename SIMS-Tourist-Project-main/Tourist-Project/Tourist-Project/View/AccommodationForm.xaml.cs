using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tourist_Project.DTO;
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
        private readonly ImageRepository imageRepository = new();
        private readonly AccommodationRepository accommodationRepository = new();
        private readonly LocationRepository locationRepository = new();
        private readonly AccommodationDTORepository accommodationDTORepository = new();
        public User LoggedInUser { get; set; }
        public Accommodation SelectedAccommodation;
        public AccommodationDTO SelectedAccommodationDTO;
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> countries { get; set; }
        public static ObservableCollection<string> cities { get; set; }
        public static ObservableCollection<Image> Images { get; set; }
        public AccommodationForm()
        {
            InitializeComponent();
            DataContext = this;
            cities = new ObservableCollection<string>();
            countries = new ObservableCollection<string>();
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            foreach (var location in Locations)
            {
                cities.Add(location.City);
                if (!countries.Contains(location.Country))
                    countries.Add(location.Country);
            }
            Title = "Create new accommodation";
        }

        public AccommodationForm(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            EnableEditing();
            SelectedAccommodation = selectedAccommodation;
            Name.Text = selectedAccommodation.Name;
            Country.Text = locationRepository.GetLocation(SelectedAccommodation.LocationId).Country.ToString();
            City.Text = locationRepository.GetLocation(SelectedAccommodation.LocationId).City.ToString();
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = imageRepository.GetImage(selectedAccommodation.ImageId).Url;
            btnSave.Visibility = Visibility.Collapsed;
            Title = "View accommodation";
        }
        public AccommodationForm(Accommodation selectedAccommodation, AccommodationDTO selectedAccommodationDTO)
        {
            InitializeComponent();
            DataContext = this;
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            cities = new ObservableCollection<string>();
            countries = new ObservableCollection<string>();
            locationRepository = new LocationRepository();
            foreach (var location in Locations)
            {
                cities.Add(location.City);
                if (!countries.Contains(location.Country))
                    countries.Add(location.Country);
            }
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationDTO = selectedAccommodationDTO;
            SelectedAccommodation.Location = locationRepository.GetLocation(selectedAccommodation.LocationId);
            Name.Text = selectedAccommodation.Name;
            Country.Text = SelectedAccommodation.Location.Country;
            City.Text = SelectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            DaysBeforeCancel.Text = selectedAccommodation.DaysBeforeCancel.ToString();
            Url.Text = imageRepository.GetImage(selectedAccommodation.ImageId).Url;
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
            if (SelectedAccommodation != null)
            {
                SelectedAccommodation.Name = Name.Text;
                SelectedAccommodation.LocationId = GetLocationId();
                SelectedAccommodation.Type = Enum.Parse<AccommodationType>(Type.Text);
                SelectedAccommodation.MaxGuestNum = int.Parse(MaxNumGuests.Text);
                SelectedAccommodation.MinStayingDays = int.Parse(MinStayingDays.Text);
                SelectedAccommodation.DaysBeforeCancel = int.Parse(DaysBeforeCancel.Text);
                SelectedAccommodation.ImageId = SelectedAccommodation.ImageIdes.First();
                Accommodation updatedAccommodation = accommodationRepository.Update(SelectedAccommodation);
                if (updatedAccommodation != null)
                {
                    /*var ownerShowWindow = new OwnerShowWindow();
                    ownerShowWindow.Show();*/
                    /*int index = OwnerShowWindow.accommodationDTOs.IndexOf(SelectedAccommodationDTO);
                    OwnerShowWindow.accommodationDTOs[index] = SelectedAccommodationDTO;
                    *//*int index = OwnerShowWindow.accommodations.IndexOf(SelectedAccommodation);
                    OwnerShowWindow.accommodations.Remove(SelectedAccommodation);
                    OwnerShowWindow.accommodations.Insert(index, updatedAccommodation);*/
                }
            }
            else
            {

                Accommodation newAccommodation = new Accommodation(Name.Text, GetLocationId(), Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(DaysBeforeCancel.Text), Images.Last().Id, FormIdesString(CreateImage()));
                Accommodation savedAccommodation = accommodationRepository.Save(newAccommodation);
                OwnerShowWindow.accommodations.Add(savedAccommodation);
                OwnerShowWindow.accommodationDTOs.Add(new AccommodationDTO(savedAccommodation, locationRepository.GetLocation(savedAccommodation.LocationId), imageRepository.GetImage(savedAccommodation.ImageId)));
            }
            Close();
        }
        private string FormIdesString(List<int> ides)
        {
            if (ides.Count > 0)
            {
                string Ides = string.Empty;
                foreach (var imageId in ides)
                {
                    Ides += imageId + ",";
                }
                Ides = Ides.Remove(Ides.Length - 1);
                return Ides;
            }
            return null;
        }
        private int GetLocationId()
        {
            return locationRepository.GetId(City.Text, Country.Text);
        }

        private List<int> CreateImage()
        {
            string Urls = Url.Text;
            List<int> ides = new();
            foreach (var url in Urls.Split(","))
            {
                Image newImage = new Image(url);
                Image savedImage = imageRepository.Save(newImage);
                ides.Add(savedImage.Id);
            }
            SelectedAccommodation.ImageIdes = ides;
            return ides;
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Country_DropDownClosed(object sender, EventArgs e)
        {
            cities.Clear();
            foreach (var location in Locations)
            {
                if (location.Country.Equals(Country.Text))
                    cities.Add(location.City);
            }
        }

        private void City_DropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in Locations)
            {
                if (location.City.Equals(City.Text))
                    Country.Text = location.Country;
            }
        }
    }
}
