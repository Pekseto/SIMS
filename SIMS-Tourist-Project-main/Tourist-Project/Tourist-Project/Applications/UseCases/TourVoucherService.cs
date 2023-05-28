﻿using System;
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
    }
}
