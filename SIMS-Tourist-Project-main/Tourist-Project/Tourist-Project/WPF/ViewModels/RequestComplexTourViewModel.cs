using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class RequestComplexTourViewModel : ViewModelBase
    {
        private readonly TourRequestService requestService = new();
        private readonly LocationService locationService = new();
        private readonly ComplexTourService complexTourService = new();
        private string selectedCountry;
        private string selectedCity;
        private ObservableCollection<string> cities;
        private int complexTourId;
        private string description;
        private string language;
        private Message message;

        public DateTime DisplayDateStart { get; set; } = DateTime.Today.AddDays(2).Date;
        public Message Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

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
        public ObservableCollection<TourRequest> TourRequests { get; set; }

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

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged();
            }
        }

        public int GuestsNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public ICommand AddTourCommand { get; set; }
        public ICommand RemoveTourCommand { get; set; }
        public ICommand PostRequestCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public RequestComplexTourViewModel(User user, NavigationStore navigationStore)
        {
            LoggedUser = user;
            complexTourId = complexTourService.GetNextRequestId();

            TourRequests = new ObservableCollection<TourRequest>();
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            SelectedCountry = Countries.First();
            Cities = new ObservableCollection<string>(locationService.GetCitiesFromCountry(SelectedCountry));
            SelectedCity = Cities.First();
            Description = string.Empty;
            Language = string.Empty;
            FromDate = DateTime.Now.AddDays(2).Date;
            UntilDate = DateTime.Now.AddDays(3).Date;


            AddTourCommand = new RelayCommand(AddTourClick, CanAddTour);
            RemoveTourCommand = new RelayCommand(RemoveTourClick, () => SelectedTourRequest != null);
            PostRequestCommand = new RelayCommand(PostRequestClick, () => TourRequests.Count > 1);
            BackCommand = new NavigateCommand<ComplexToursViewModel>(navigationStore, () => new ComplexToursViewModel(user, navigationStore));
        }

        private void PostRequestClick()
        {
            complexTourService.Save(new ComplexTour(complexTourId, LoggedUser.Id));
            foreach (var tourRequest in TourRequests)
            {
                requestService.Save(tourRequest);
            }
            TourRequests.Clear();

            _ = ShowMessageAndHide(new Message(true, "You have successfully posted a complex tour request!"));
        }

        private void RemoveTourClick()
        {
            TourRequests.Remove(SelectedTourRequest);
        }

        private bool CanAddTour()
        {
            return Description != string.Empty && Language != string.Empty && GuestsNumber != 0;
        }

        private void AddTourClick()
        {
            var locationId = locationService.GetId(SelectedCity, SelectedCountry);
            TourRequest newRequest =
                new(locationId, Description, Language, GuestsNumber, FromDate, UntilDate, LoggedUser.Id, DateTime.Now, complexTourId)
                {
                    Location = locationService.Get(locationId)
                };
            TourRequests.Add(newRequest);

            Description = string.Empty;
            Language = string.Empty;
        }

        private async Task ShowMessageAndHide(Message message)
        {
            Message = message;
            await Task.Delay(5000);
            Message = new Message();
        }
    }
}
