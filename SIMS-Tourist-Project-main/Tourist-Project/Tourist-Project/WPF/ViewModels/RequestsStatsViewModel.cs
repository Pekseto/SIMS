using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class RequestsStatsViewModel : ViewModelBase
    {
        private readonly TourRequestService requestService = new();
        private readonly LocationService locationService = new();
        private string selectedCountry;
        private string selectedCity;
        private ObservableCollection<string> cities;
        public User LoggedUser { get; set; }

        public ObservableCollection<string> Countries { get; set; }

        public ObservableCollection<string> Cities
        {
            get => cities;
            set
            {
                if (value != cities)
                {
                    cities = value;
                    SelectedCity = value.First();
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TourRequest> Requests { get; set; }

        public string SelectedCountry
        {
            get => selectedCountry;
            set
            {
                if (value != selectedCountry)
                {
                    selectedCountry = value;
                    Cities = locationService.GetCitiesFromCountry(SelectedCountry);
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        public string SelectedCity
        {
            get => selectedCity;
            set
            {
                if (value != selectedCity)
                {
                    selectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description { get; set; }
        public string Language { get; set; }
        public int GuestsNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }

        public ICommand PostRequestCommand { get; set; }


        public RequestsStatsViewModel(User user, NavigationStore navigationStore)
        {
            LoggedUser = user;

            Requests = new ObservableCollection<TourRequest>(GetAllRequests());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            SelectedCountry = Countries.First();
            Cities = new ObservableCollection<string>(locationService.GetCitiesFromCountry(SelectedCountry));
            SelectedCity = Cities.First();
            Description = string.Empty;
            Language = string.Empty;
            FromDate = DateTime.Now.AddDays(1).Date;
            UntilDate = DateTime.Now.AddDays(2).Date;

            PostRequestCommand = new RelayCommand(PostRequestClick, CanPostRequest);
        }

        private List<TourRequest> GetAllRequests()
        {
            var retVal = requestService.GetAllForUser(LoggedUser.Id);
            foreach (var tourRequest in retVal)
            {
                tourRequest.Location = locationService.Get(tourRequest.LocationId);
            }
            return retVal;
        }

        private bool CanPostRequest()
        {
            return Description != string.Empty && Language != string.Empty && GuestsNumber != 0;
        }

        private void PostRequestClick()
        {
            var locationId = locationService.GetId(SelectedCity, SelectedCountry);
            TourRequest newRequest =
                new(locationId, Description, Language, GuestsNumber, FromDate, UntilDate, LoggedUser.Id)
                {
                    Location = locationService.Get(locationId)
                };
            requestService.Save(newRequest);
            Requests.Add(newRequest);
        }

    }
}
