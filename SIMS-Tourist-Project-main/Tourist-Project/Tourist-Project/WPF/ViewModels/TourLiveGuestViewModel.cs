using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourLiveGuestViewModel
    {
        public TourDTO SelectedTour { get; set; }
        public ObservableCollection<TourPoint> TourPoints { get; set; }

        private readonly TourPointRepository tourPointRepository = new();

        public TourLiveGuestViewModel(TourDTO tour)
        {
            SelectedTour = tour;
            TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(SelectedTour.Id));
        }
    }
}
