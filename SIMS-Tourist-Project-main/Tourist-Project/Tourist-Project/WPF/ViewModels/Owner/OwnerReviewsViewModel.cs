using System.Collections.ObjectModel;
using System.Linq;
using Tourist_Project.Applications.UseCases;

namespace Tourist_Project.WPF.ViewModels.Owner;

public class OwnerReviewsViewModel
{
    public static ObservableCollection<AccommodationRatingViewModel> Reviews { get; set; }
    private readonly AccommodationRatingService ratingService = new();
    public AccommodationRatingViewModel SelectedReview { get; set; }
    public OwnerReviewsViewModel()
    {
        Reviews = new ObservableCollection<AccommodationRatingViewModel>(ratingService.GetAll().Select(rating => new AccommodationRatingViewModel(rating)));
    }
}