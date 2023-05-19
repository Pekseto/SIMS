using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels.Owner;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class ScheduleRenovationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccommodationViewModel> accommodationViewModel;

        public ObservableCollection<AccommodationViewModel> AccommodationViewModel
        {
            get => accommodationViewModel;
            set
            {
                if(value == accommodationViewModel) return;
                accommodationViewModel = value;
                OnPropertyChanged();
            }
        }

        private string renovationLength;

        public string RenovationLength
        {
            get => renovationLength;
            set
            {
                if(value == renovationLength) return;
                renovationLength = value;
                OnPropertyChanged();
            }
        }

        private DateSpan requestedDateSpan;
        public DateSpan RequestedDateSpan
        {
            get => requestedDateSpan;
            set
            {
                if(value == requestedDateSpan) return;
                requestedDateSpan = value;
                OnPropertyChanged();
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                if (value == description) return;
                description = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateSpan> possibleDateSpans;

        public ObservableCollection<DateSpan> PossibleDateSpans
        {
            get => possibleDateSpans;
            set
            {
                if(value == possibleDateSpans) return;
                possibleDateSpans = value;
                OnPropertyChanged();
            }
        }

        public DateSpan SelectedDateSpan { get; set; }

        private readonly RenovationService renovationService = new ();

        private ObservableCollection<DateTime> blackoutStartDates;
        public ObservableCollection<DateTime> BlackoutStartDates
        {
            get => blackoutStartDates;
            set
            {
                if (value == blackoutStartDates) return;
                blackoutStartDates = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DateTime> blackoutEndDates;
        public ObservableCollection<DateTime> BlackoutEndDates
        {
            get => blackoutEndDates;
            set
            {
                if(value == blackoutEndDates) return;
                blackoutEndDates = value;
                OnPropertyChanged();
            }
        }

        public DateTime Today { get; set; }
        public ScheduleRenovation ScheduleRenovation;

        public ICommand FindCommand { get; set; }
        public ICommand RenovateCommand { get; set; }
        public ScheduleRenovationViewModel() {}

        public ScheduleRenovationViewModel(AccommodationViewModel accommodationViewModel, ScheduleRenovation scheduleRenovation)
        {
            ScheduleRenovation = scheduleRenovation;
            AccommodationViewModel = new ObservableCollection<AccommodationViewModel> { accommodationViewModel };
            FindCommand = new RelayCommand(Find, CanFind);
            RenovateCommand = new RelayCommand(Renovate, CanRenovate);
            RequestedDateSpan = new DateSpan();
            Today = DateTime.Now;
            PossibleDateSpans = new ObservableCollection<DateSpan>();
        }

        #region CommandLogic

        public void Find()
        {
            foreach (var dateSpan in renovationService.FindDateSpans(RequestedDateSpan, Convert.ToInt32(renovationLength), AccommodationViewModel.First().Accommodation.Id))
            {
                possibleDateSpans.Add(dateSpan);
            }
        }

        public bool CanFind()
        {
            return Convert.ToInt32(RenovationLength) > 0 && RequestedDateSpan.EndingDate > RequestedDateSpan.StartingDate;
        }

        public void Renovate()
        {
            SelectedDateSpan ??= possibleDateSpans.FirstOrDefault();
            renovationService.Create(new Renovation(AccommodationViewModel.First().Accommodation.Id, SelectedDateSpan, Description));
            ScheduleRenovation.Close();
        }

        public bool CanRenovate()
        {
            return SelectedDateSpan != null && !string.IsNullOrWhiteSpace(Description);
        }

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}
