using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourLiveGuestViewModel
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public TourPoint SelectedTourPoint { get; set; }

        private readonly TourPointRepository tourPointRepository = new();

        public TourLiveGuestViewModel(User user, int selectedTourId)
        {
            LoggedInUser = user;

            TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(selectedTourId));
        }
    }
}
