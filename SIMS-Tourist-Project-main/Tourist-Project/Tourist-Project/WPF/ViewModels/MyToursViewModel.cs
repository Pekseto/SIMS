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

namespace Tourist_Project.WPF.ViewModels
{
    public class MyToursViewModel
    {
        private readonly TourReservationService reservationService;
        private readonly TourService tourService;
        private readonly LocationService locationService;
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourDTO> FutureTours { get; set; }
        public ObservableCollection<TourDTO> TodaysTours { get; set; }
        public TourDTO SelectedTodayTour { get; set; }

        public MyToursViewModel(User user) 
        {
            tourService = new TourService();
            reservationService = new TourReservationService();
            locationService = new LocationService();

            LoggedInUser = user;
            FutureTours = new ObservableCollection<TourDTO>();
            TodaysTours = new ObservableCollection<TourDTO>();

            foreach(TourReservation t in reservationService.GetAll())
            {
                if(t.UserId == LoggedInUser.Id && tourService.GetAll().Find(x => x.Id == t.TourId).StartTime > DateTime.Today)
                {
                    Tour tour = tourService.GetAll().Find(x => x.Id == t.TourId);
                    var tourDTO = new TourDTO(tour)
                    {
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    FutureTours.Add(tourDTO);
                }
                else if(t.UserId == LoggedInUser.Id && tourService.GetAll().Find(x => x.Id == t.TourId).StartTime == DateTime.Today)
                {
                    Tour tour = tourService.GetAll().Find(x => x.Id == t.TourId);
                    var tourDTO = new TourDTO(tour)
                    {
                        Location = locationService.GetAll().Find(x => x.Id == tour.LocationId)
                    };
                    TodaysTours.Add(tourDTO);
                }

            }
        }
    }
}
