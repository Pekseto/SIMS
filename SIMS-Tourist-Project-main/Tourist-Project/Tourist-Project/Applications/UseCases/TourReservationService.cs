using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourReservationService
    {
        private static readonly Injector injector = new();

        private readonly ITourReservationRepository repository = injector.CreateInstance<ITourReservationRepository>();
        private readonly UserService userService = new();

        public TourReservationService()
        {

        }

        public List<TourReservation> GetAllByTourId(int tourId)
        {
            return repository.GetAllByTourId(tourId);
        }
        public List<User> GetUsersOnTour(int tourId)
        {
            return repository.GetAllByTourId(tourId).Select(user => userService.GetOne(user.Id)).ToList();
        }

        public int[] CountingTourists(int tourId)
        {
            var counterYounger = 0;
            var counterBetween = 0;
            var counterOlder = 0;
            foreach (var user in GetUsersOnTour(tourId))
            {
                switch (DateTime.Now.Subtract(user.BirtDate).TotalDays / 365.25)
                {
                    case < 18:
                        counterYounger++;
                        break;
                    case > 50:
                        counterOlder++;
                        break;
                    default:
                        counterBetween++;
                        break;
                }
            }
            return new int[] { counterYounger, counterBetween, counterOlder };
        }

        public double WithVoucherPercent(int tourId)
        {
            return (double)repository.NumberWithVoucher(tourId)/(double)GetUsersOnTour(tourId).Count * 100;
        }

        public double WithOutVoucherPercent(int tourId)
        {
            return (double)repository.NumberWithoutVoucher(tourId) / (double)GetUsersOnTour(tourId).Count * 100;
        }
    }
}
