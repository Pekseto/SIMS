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

        public TourVoucherService() 
        {
        }
        public TourVoucher Create(TourVoucher voucher)
        {
            return voucherRepository.Save(voucher);
        }

        public void VouchersDistribution(int id)
        {
            foreach(var reservation in reservationService.GetAllByTourId(id))
            {
                TourVoucher tourVoucher = new TourVoucher(reservation.UserId, reservation.TourId);
                Create(tourVoucher);
            }
        }
    }
}
