using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tourist_Project.Applications.UseCases;

namespace Tourist_Project.WPF.ViewModels.Owner;

public class OwnerReviewsViewModel : INotifyPropertyChanged
{
    private ObservableCollection<AccommodationRatingViewModel> reviews;
    public ObservableCollection<AccommodationRatingViewModel> Reviews
    {
        get => reviews;
        set
        {
            if(value == reviews) return;
            reviews = value;
            OnPropertyChanged("Reviews");
        }
    }
    private readonly AccommodationRatingService ratingService = new();
    public AccommodationRatingViewModel SelectedReview { get; set; }
    public OwnerReviewsViewModel()
    {
        Reviews = new ObservableCollection<AccommodationRatingViewModel>(ratingService.GetAll().Select(rating => new AccommodationRatingViewModel(rating)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}