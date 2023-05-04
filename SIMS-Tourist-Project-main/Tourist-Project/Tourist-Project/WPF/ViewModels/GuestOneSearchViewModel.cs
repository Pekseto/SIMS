using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestOneSearchViewModel
    {

        private Window _window;
        #region SearchDataDisplay
        public ObservableCollection<String> Countries { get; set; } = new ObservableCollection<String>();
        public ObservableCollection<String> Cities { get; set; } = new ObservableCollection<String>();
        public ObservableCollection<String> AccommodationTypes { get; set; } = new ObservableCollection<String>();
        #endregion

        private LocationRepository _locationRepository =  new();
        private AccommodationRepository _accommodationRepository = new();

        #region SearchParameters
        public String AccommodationName { get; set; }
        public String SelectedType { get; set; }

        public String SelectedCountry { get; set; }
        public String SelectedCity { get; set; }

        public int SearchedCancelationThreshold { get; set; }
        public int SearchedGuestNum { get; set; }

        public String SelectedTypeChanged { get; set; }
        public String SelectedCityChanged { get; set; }
        public String SelectedCountryChanged { get; set; }

        #endregion

        public GuestOneSearchViewModel(Window window)
        {
            _window = window;
            Countries = GetCountries();
            Cities = GetCities();
            AccommodationTypes = GetAccommodationTypes();

        }

        #region GettingDataForDisplay
        private ObservableCollection<String> GetCountries()
        {
            foreach(var location in _locationRepository.GetAll())
            {
                if (Countries.Contains(location.Country))
                {
                    continue;
                }
                else
                {
                    Countries.Add(location.Country);
                }
            }
            return Countries;
        }

        private ObservableCollection<String> GetCities()
        {
            foreach(var location in _locationRepository.GetAll())
            {
                Cities.Add(location.City);
            }
            return Cities;
        }


        private ObservableCollection<String> GetAccommodationTypes()
        {
            foreach(var accommodation in _accommodationRepository.GetAll())
            {
                AccommodationTypes.Add(accommodation.Type.ToString());
            }
            return AccommodationTypes;
        }
        #endregion
    }
}
