using System;
using System.Collections.ObjectModel;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

public class OwnerReviewsViewModel
{
	public static ObservableCollection<AccommodationRating> Reviews { get; set; }
    private readonly AccommodationRatingService ratingService = new();
    public OwnerReviewsViewModel()
    {
        Reviews = new ObservableCollection<AccommodationRating>(ratingService.GetAll());
    }
}
