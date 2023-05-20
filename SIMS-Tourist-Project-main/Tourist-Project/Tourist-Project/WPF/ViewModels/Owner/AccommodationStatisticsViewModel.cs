using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class AccommodationStatisticsViewModel : INotifyPropertyChanged
    {
        public AccommodationViewModel AccommodationViewModel { get; set; }
        private ObservableCollection<AccommodationStatistics> accommodationStatistics;
        public ObservableCollection<AccommodationStatistics> AccommodationStatistics
        {
            get => accommodationStatistics;
            set
            {
                if(value == accommodationStatistics) return;
                accommodationStatistics = value;
                OnPropertyChanged();
            }
        }
        private readonly AccommodationStatisticsService accommodationStatisticsService = new();
        public List<int> Years { get; set; }
        public AccommodationStatisticsViewModel(AccommodationViewModel accommodationViewModel)
        {
            AccommodationViewModel = accommodationViewModel;
            Years = accommodationStatisticsService.GetYears(AccommodationViewModel.Accommodation.Id);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}