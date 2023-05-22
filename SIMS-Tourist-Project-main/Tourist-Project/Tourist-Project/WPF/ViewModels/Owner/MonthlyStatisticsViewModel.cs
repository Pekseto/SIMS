using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels.Owner;

public class MonthlyStatisticsViewModel : INotifyPropertyChanged
{
    public AccommodationViewModel AccommodationViewModel { get; set; }

    private ObservableCollection<AccommodationStatistics> accommodationStatistics;
    public ObservableCollection<AccommodationStatistics> AccommodationStatistics
    {
        get => accommodationStatistics;
        set
        {
            if (value == accommodationStatistics) return;
            accommodationStatistics = value;
            OnPropertyChanged();
        }
    }
    private readonly AccommodationStatisticsService accommodationStatisticsService = new();
    public int MostOccupiedMonth { get; set; }

    public ICommand SwitchToYearlyCommand { get; set; }
    public MonthlyStatisticsViewModel(AccommodationViewModel accommodationViewModel, int year)
    {
        AccommodationViewModel = accommodationViewModel;
        AccommodationStatistics = new ObservableCollection<AccommodationStatistics>();
        for (var i = 1; i < 13; i++)
        {
            AccommodationStatistics.Add(new AccommodationStatistics(accommodationStatisticsService.GetTotalReservation(AccommodationViewModel.Accommodation.Id, year, i),
                accommodationStatisticsService.GetTotalCancelledReservations(AccommodationViewModel.Accommodation.Id, year, i),
                    accommodationStatisticsService.GetTotalRescheduledReservations(AccommodationViewModel.Accommodation.Id, year, i),
                        accommodationStatisticsService.GetTotalRenovationRecommendations(AccommodationViewModel.Accommodation.Id, year, i),
                            accommodationStatisticsService.GetOccupancy(AccommodationViewModel.Accommodation.Id, year, i),
                            i.ToString()));
        }
        MostOccupiedMonth = accommodationStatisticsService.GetMostOccupiedMonth(AccommodationViewModel.Accommodation.Id, year);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
