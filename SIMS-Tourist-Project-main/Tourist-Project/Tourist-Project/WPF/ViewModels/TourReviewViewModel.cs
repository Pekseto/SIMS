using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReviewViewModel : INotifyPropertyChanged
    {
        private readonly TourReviewService ratingService;
        private readonly ImageService imageService;
        private readonly TourAttendanceService attendanceService;
        public int KnowledgeRating { get; set; }
        public int LanguageRating { get; set; }
        public int EntertainmentRating { get; set; }
        public string Comment { get; set; }
        private string imageURL;
        public string ImageURL
        {
            get => imageURL;
            set
            {
                if(imageURL != value)
                {
                    imageURL = value;
                    OnPropertyChanged(nameof(ImageURL));
                }
            }
        }
        public List<Image> Images { get; set; }

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
            attendanceService = new TourAttendanceService();

            ImageURL = string.Empty;
            Images = new List<Image>();

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
            Images.Add(image);
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
            if(attendanceService.WasUserPresent(MainWindow.LoggedInUser.Id, SelectedTourId))
            {
                TourReview tourReview = new TourReview(LoggedInUser.Id, SelectedTourId, KnowledgeRating, LanguageRating, EntertainmentRating, Comment);
                ratingService.Save(tourReview);

                foreach (var image in Images)
                {
                    imageService.Save(image);
                }

                MessageBox.Show("Successfully rated the tour");
            }
            else
            {
                MessageBox.Show("You weren't present on this tour, so you can't review it!");
            }


            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
