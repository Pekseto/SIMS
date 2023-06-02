using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class TourVoucherService
    {
        private static readonly Injector injector = new();

        private readonly ITourVoucherRepository voucherRepository = injector.CreateInstance<ITourVoucherRepository>();

        private readonly TourReservationService reservationService = new();
        private readonly TourAttendanceService attendanceService = new();
        private readonly UserService userService = new();

        public TourVoucherService() 
        {
        }
        public TourVoucher Create(TourVoucher voucher)
        {
            return voucherRepository.Save(voucher);
        }

        public void Delete(int voucherId)
        {
            voucherRepository.Delete(voucherId);
        }

        public void VouchersDistribution(int id)
        {
            foreach(var reservation in reservationService.GetAllByTourId(id))
            {
                var tourVoucher = new TourVoucher(reservation.UserId, reservation.TourId, "Guide cancellation"); //GuideId dodati
                Create(tourVoucher);
            }
        }

        public void VoucherDistributionForAnyTour(Tour tour)
        {
            foreach (var reservation in reservationService.GetAllByTourId(tour.Id))
            {
                var tourVoucher = new TourVoucher(reservation.UserId, reservation.TourId, "Guide cancellation");
                Create(tourVoucher);
            }
        }

        public List<TourVoucher> GetAllForUser(int userId)
        {
            return voucherRepository.GetAllForUser(userId);
        }

        public void DeleteInvalidVouchers(int userId)
        {
            voucherRepository.DeleteInvalidVouchers(userId);
        }

        public void ClaimFiveToursInAYearVoucher(int userId)
        {
            var user = userService.GetOne(userId);

            if (user.VoucherAcquiredDate.AddYears(1) < DateTime.Now)
            {
                user.AcquiredYearlyVoucher = false;
                userService.Update(user);
            }

            if (user.AcquiredYearlyVoucher) return;
            
            var pastYearAttendancesCount = attendanceService.GetUsersPastYearAttendancesCount(userId);
            if (pastYearAttendancesCount >= 5)
            {
                var voucher = new TourVoucher(userId, "5 tours in 1 year", DateTime.Now.AddMonths(6));
                Create(voucher);

                user.VoucherAcquiredDate = DateTime.Now;
                user.AcquiredYearlyVoucher = true;
                userService.Update(user);
            }

        }
    }
}
