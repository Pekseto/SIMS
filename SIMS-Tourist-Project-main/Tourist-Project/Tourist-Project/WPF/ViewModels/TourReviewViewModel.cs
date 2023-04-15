using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReviewViewModel
    {
        private readonly TourReviewService ratingService;
        private readonly ImageService imageService;
        public int KnowledgeRating { get; set; }
        public int LanguageRating { get; set; }
        public int EntertainmentRating { get; set; }
        public string Comment { get; set; }
        public string ImageURL { get; set; }

        public User LoggedInUser { get; set; }
        //public Tour SelectedTour { get; set; }
        public int SelectedTourId { get; set; }

        public ICommand RateCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public TourReviewViewModel(User user, int tourId)
        {
            LoggedInUser = user;
            SelectedTourId = tourId;
            ratingService = new TourReviewService();
            imageService = new ImageService();

            RateCommand = new RelayCommand(OnRateClick, CanRate);
            AddCommand = new RelayCommand(OnAddClick, CanAdd);

            Comment = string.Empty;
            ImageURL = string.Empty;
        }

        private bool CanAdd()
        {
            if(ImageURL !=  string.Empty)
            {
                return true;
            }
            return false;
        }

        private void OnAddClick()
        {
            Image image = new(ImageURL);
            imageService.Save(image); 
            ImageURL = string.Empty;
        }

        private bool CanRate()
        {
            if(KnowledgeRating != 0 && LanguageRating != 0 && EntertainmentRating != 0 && Comment != string.Empty)
            {
                return true;
            }
            return false;
        }

        private void OnRateClick()
        {
            TourReview tourReview = new TourReview(LoggedInUser.Id, SelectedTourId, KnowledgeRating, LanguageRating, EntertainmentRating, Comment);
            ratingService.Save(tourReview);
        }
    }
}
