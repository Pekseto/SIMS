using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.DTO;
using Tourist_Project.Serializer;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels.Guide
{
    public class TourReviewsGuideViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TourReviewDTO> tourReview;

        public ObservableCollection<TourReviewDTO> TourReviews
        {
            get { return tourReview; }
            set
            {
                tourReview = value;
                OnPropertyChanged("TourReviews");
            }
        }
        private readonly TourReviewService tourReviewService = new();

        private Window window;
        public Tour Tour { get; set; }
        public TourReviewDTO SelectedReview { get; set; }
        public TourReview TourReview { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Command
        public ICommand AcceptCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public ICommand BackCommand { get; set; }

        #endregion

        public TourReviewsGuideViewModel(Window window, Tour tour)
        {
            this.window = window;
            Tour = tour;

            AcceptCommand = new RelayCommand(Accept, CanAccept);
            DeclineCommand = new RelayCommand(Decline, CanDecline);
            BackCommand = new RelayCommand(Back);

            TourReviews = new ObservableCollection<TourReviewDTO>(tourReviewService.GetAllReviewDtos(tour.Id));
            
        }

        private bool CanAccept()
        {
            return SelectedReview != null;
        }

        private void Accept()
        {
            TourReview = tourReviewService.GetOne(SelectedReview.Id);
            TourReview.Valid = ValidStatus.Valid;
            UpdateData();
        }

        private bool CanDecline()
        {
            return SelectedReview != null;
        }

        private void Decline()
        {
            TourReview = tourReviewService.GetOne(SelectedReview.Id);
            TourReview.Valid = ValidStatus.NotValid;
            UpdateData();
        }

        private void Back()
        {
            window.Close();
        }

        private void UpdateData()
        {
            tourReviewService.Update(TourReview);
            TourReviews.Clear();
            foreach (var review in tourReviewService.GetAllReviewDtos(Tour.Id))
            {
                TourReviews.Add(review);
            }
        }
    }
}
