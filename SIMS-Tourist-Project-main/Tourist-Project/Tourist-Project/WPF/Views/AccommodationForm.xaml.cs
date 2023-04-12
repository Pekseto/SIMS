using System;
using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;
using Tourist_Project.WPF.ViewModels;
using Image = Tourist_Project.Domain.Models.Image;

namespace Tourist_Project.WPF.Views
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
        public AccommodationDTO SelectedAccommodationDto;
        public static ObservableCollection<Location> Locations { get; set; } = new();
        public static ObservableCollection<string> Countries { get; set; } = new();
        public static ObservableCollection<string> Cities { get; set; } = new();
        public static ObservableCollection<Image> Images { get; set; } = new();
        public AccommodationForm()
        {
            InitializeComponent();
            DataContext = new CreateAccommodationViewModel(this);
        }


        public AccommodationForm(Accommodation selectedAccommodation, AccommodationDTO selectedAccommodationDto)
        {
            InitializeComponent();
            DataContext = this;
            Locations = new ObservableCollection<Location>(locationRepository.GetAll());
            Images = new ObservableCollection<Image>(imageRepository.GetAll());
            Load(selectedAccommodation, selectedAccommodationDto);
            Title = "Update accommodation";
        }

        private void Load(Accommodation selectedAccommodation, AccommodationDTO selectedAccommodationDto)
        {
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationDto = selectedAccommodationDto;
            SelectedAccommodation.Location = locationRepository.GetById(selectedAccommodation.LocationId);
            Name.Text = selectedAccommodation.Name;
            Country.Text = SelectedAccommodation.Location.Country;
            City.Text = SelectedAccommodation.Location.City;
            Type.Text = selectedAccommodation.Type.ToString();
            MaxNumGuests.Text = selectedAccommodation.MaxGuestNum.ToString();
            MinStayingDays.Text = selectedAccommodation.MinStayingDays.ToString();
            CancellationThreshold.Text = selectedAccommodation.CancellationThreshold.ToString();
            Url.Text = imageRepository.GetById(selectedAccommodation.ImageId).Url;
        }

        /*private void Confirm(object sender, RoutedEventArgs e)
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
            Accommodation newAccommodation = new(Name.Text, GetLocationId(), Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(CancellationThreshold.Text), Images.Last().Id, FormIdesString(CreateImage()));
            var savedAccommodation = accommodationRepository.Save(newAccommodation);

            OwnerShowWindow.Accommodations.Add(savedAccommodation);
            OwnerShowWindow.AccommodationDtos.Add(new AccommodationDTO(savedAccommodation, locationRepository.GetById(savedAccommodation.LocationId), imageRepository.GetById(savedAccommodation.ImageId)));
        }
        */
        private void UpdateSelectedAccommodation()
        {
            SelectedAccommodation.Name = Name.Text;
            //SelectedAccommodation.LocationId = GetLocationId();
            SelectedAccommodation.Type = Enum.Parse<AccommodationType>(Type.Text);
            SelectedAccommodation.MaxGuestNum = int.Parse(MaxNumGuests.Text);
            SelectedAccommodation.MinStayingDays = int.Parse(MinStayingDays.Text);
            SelectedAccommodation.CancellationThreshold = int.Parse(CancellationThreshold.Text);
            //SelectedAccommodation.ImageIds = CreateImage();
            SelectedAccommodation.ImageIdsCSV = Url.Text;
            _ = accommodationRepository.Update(SelectedAccommodation);
        }
        /*
        private static string? FormIdesString(List<int> ids)
        {
            if (ids.Count <= 0) return null;
            var ides = ids.Aggregate(string.Empty, (current, imageId) => current + (imageId + ","));
            ides = ides.Remove(ides.Length - 1);
            return ides;
        }
        private int GetLocationId()
        {
            return locationRepository.GetId(City.Text, Country.Text);
        }

        private List<int> CreateImage()
        {
            List<int> ides = new();
            if (!Url.Text.Contains(",")) return ides;
            foreach (var url in Url.Text.Split(","))
            {
                Image newImage = new(url);
                var savedImage = imageRepository.Save(newImage);
                ides.Add(savedImage.Id);
            }

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
        }*/
        private void CountryDropDownClosed(object sender, EventArgs e)
        {
            Cities.Clear();
            foreach (var location in Locations)
            {
                if (location.Country.Equals(Country.Text))
                    Cities.Add(location.City);
            }
        }
        private void CityDropDownClosed(object sender, EventArgs e)
        {
            foreach (var location in Locations)
            {
                if (location.City.Equals(City.Text))
                    Country.Text = location.Country;
            }
        }/*
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }*/
    }
}
