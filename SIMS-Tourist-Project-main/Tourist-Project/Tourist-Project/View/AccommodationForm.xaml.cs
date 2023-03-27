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
        public Accommodation SelectedAccommodation;
        public AccommodationDTO SelectedAccommodationDTO;
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ObservableCollection<Image> Images { get; set; } = new();
        public AccommodationForm()
        {
            InitializeComponent();
            DataContext = this;
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            InitializeCitiesAndCountries();
            Title = "Create new accommodation";
        }


        public AccommodationForm(Accommodation selectedAccommodation, AccommodationDTO selectedAccommodationDTO)
        {
            InitializeComponent();
            DataContext = this;
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            Load(selectedAccommodation, selectedAccommodationDTO);
            Title = "Update accommodation";
        }

        private void Load(Accommodation selectedAccommodation, AccommodationDTO selectedAccommodationDTO)
        {
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationDTO = selectedAccommodationDTO;
            SelectedAccommodation.Location = locationRepository.GetById(selectedAccommodation.LocationId);
            name.Text = selectedAccommodation.Name;
            country.Text = SelectedAccommodation.Location.Country;
            city.Text = SelectedAccommodation.Location.City;
            type.Text = selectedAccommodation.Type.ToString();
            maxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            minStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            cancellationThreshold.Text = selectedAccommodation.CancellationThreshold.ToString();
            url.Text = imageRepository.Get(selectedAccommodation.ImageId).Url;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation != null)
            {
                UpdateSelectedAccommodation();
            }
            else
            {
                CreateAccommodation();
            }
            Close();
        }

        private void CreateAccommodation()
        {
            Accommodation newAccommodation = new(name.Text, GetLocationId(), Enum.Parse<Tourist_Project.Model.AccommodationType>(type.Text), int.Parse(maxNumGuests.Text), int.Parse(minStayingDays.Text), int.Parse(cancellationThreshold.Text), Images.Last().Id, FormIdesString(CreateImage()));
            Accommodation savedAccommodation = accommodationRepository.Save(newAccommodation);

            OwnerShowWindow.Accommodations.Add(savedAccommodation);
            OwnerShowWindow.AccommodationDTOs.Add(new(savedAccommodation, locationRepository.GetById(savedAccommodation.LocationId), imageRepository.Get(savedAccommodation.ImageId)));
        }

        private void UpdateSelectedAccommodation()
        {
            SelectedAccommodation.Name = name.Text;
            SelectedAccommodation.LocationId = GetLocationId();
            SelectedAccommodation.Type = Enum.Parse<Tourist_Project.Model.AccommodationType>(type.Text);
            SelectedAccommodation.MaxGuestNum = int.Parse(maxNumGuests.Text);
            SelectedAccommodation.MinStayingDays = int.Parse(minStayingDays.Text);
            SelectedAccommodation.CancellationThreshold = int.Parse(cancellationThreshold.Text);
            SelectedAccommodation.ImageIdsCSV = url.Text;
            _ = accommodationRepository.Update(SelectedAccommodation);
        }

        private static string? FormIdesString(List<int> ides)
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
            return locationRepository.GetId(city.Text, country.Text);
        }

        private List<int> CreateImage()
        {
            List<int> ides = new();
            foreach (var url in url.Text.Split(",")) 
            {
                Image newImage = new(url);
                Image savedImage = imageRepository.Save(newImage);
                ides.Add(savedImage.Id);
            }
            SelectedAccommodation.ImageIds = ides;
            return ides;
        }
        private static void InitializeCitiesAndCountries()
        {
            foreach (var location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                    Countries.Add(location.Country);
            }
        }
        private void CountryDropDownClosed(object sender, EventArgs e)
        {
            Cities.Clear();
            foreach (var location in Locations)
            {
                if (location.Country.Equals(country.Text))
                    Cities.Add(location.City);
            }
        }
        private void CityDropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in Locations)
            {
                if (location.City.Equals(city.Text))
                    country.Text = location.Country;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
