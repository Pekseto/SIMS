using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReviewDTO
    {

    }

    public class TourReviewsGuideViewModel
    {
        public static ObservableCollection<TourReview> TourReviews { get; set; }
        private readonly TourReviewService tourReviewService = new();
        private Window window;
        public Tour Tour { get; set; }
        public TourReview SelectedReview {get; set; }
        public TourReview TourReview { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public TourReviewsGuideViewModel(Window window, Tour tour)
        {
            this.window = window;
            Tour = tour;

            AcceptCommand = new RelayCommand(Accept, CanAccept);
            DeclineCommand = new RelayCommand(Decline, CanDecline);
            BackCommand = new RelayCommand(Back, CanBack);

            SelectedReview = null;
            TourReviews = new ObservableCollection<TourReview>(tourReviewService.GetAllByTourId(Tour.Id));
        }

        private bool CanAccept()
        {
            return SelectedReview != null;
        }

        private void Accept()
        {
            SelectedReview.Valid = ValidStatus.Valid;
            tourReviewService.Update(SelectedReview);
            TourReviews.Clear();
            foreach (TourReview review in tourReviewService.GetAllByTourId(Tour.Id))
            {
                TourReviews.Add(review);
            }
        }

        private bool CanDecline()
        {
            return SelectedReview != null;
        }

        private void Decline()
        {
            SelectedReview.Valid = ValidStatus.NotValid;
            tourReviewService.Update(SelectedReview);
            TourReviews.Clear();
            foreach (TourReview review in tourReviewService.GetAllByTourId(Tour.Id))
            {
                TourReviews.Add(review);
            }
        }

        private bool CanBack()
        {
            return true;
        }

        private void Back()
        {
            window.Close();
        }
    }
}
