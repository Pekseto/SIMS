using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourHistoryViewModel
    {
        private readonly TourService tourService;
        public ObservableCollection<TourDTO> Tours { get; set; }
        public TourDTO? SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        public ICommand ReviewCommand { get; set; }

        public TourHistoryViewModel(User user)
        {
            LoggedInUser = user;

            tourService = new TourService();

            ReviewCommand = new RelayCommand(OnReviewClick, CanReview);

            Tours = new ObservableCollection<TourDTO>(tourService.GetAllPastTours(MainWindow.LoggedInUser.Id));
        }

        private bool CanReview()
        {
            if(SelectedTour != null)
            {
                return true;
            }
            return false;
        }

        private void OnReviewClick()
        {
            var reviewWindow = new TourReviewView(LoggedInUser, SelectedTour.Id);
            reviewWindow.Show();
        }
    }
}
