using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReservationViewModel : ViewModelBase
    {
        private readonly VoucherService voucherService;
        private readonly TourReservationService reservationService;
        private readonly TourAttendanceService attendanceService;
        public int GuestsNumber { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public User LoggedUser { get; set; }
        private readonly NavigationStore navigationStore;
        public TourDTO SelectedTour { get; set; }
        public ICommand ReserveCommand { get; set; }

        public TourReservationViewModel(User user, TourDTO tour, NavigationStore navigationStore)
        {
            LoggedUser = user;
            SelectedTour = tour;
            this.navigationStore = navigationStore;

            ReserveCommand = new RelayCommand(OnReserveClick);

            voucherService = new VoucherService();
            reservationService = new TourReservationService();
            attendanceService = new TourAttendanceService();

            Vouchers = new ObservableCollection<Voucher>(voucherService.GetAllForUser(user.Id));

        }

        private void OnReserveClick()
        {
            if (GuestsNumber > 0)
            {
                int tourCapacityLeft = SelectedTour.SpotsLeft;

                if (tourCapacityLeft == 0)
                {
                    MessageBox.Show("This tours capacity is full at the moment.\n" +
                                    "Here are some other tours on the same location!");
                    //DisplaySimilarTours(SelectedTour);     DODATI OVU FUNKCIONALNOST
                }
                else if (tourCapacityLeft < GuestsNumber)
                {
                    MessageBox.Show("Unfortunately, we can't accept that many guests at the moment.\n" +
                                    "You are welcome to lower the amount of people coming with you!\n" +
                                    "Capacity left: " + tourCapacityLeft.ToString());
                }
                else
                {
                    SelectedTour.SpotsLeft -= GuestsNumber;
                    var tourReservation = new TourReservation(LoggedUser.Id, SelectedTour.Id, GuestsNumber, SelectedVoucher != null ? true : false);
                    reservationService.Save(tourReservation);
                    var tourAttendance = new TourAttendance(LoggedUser.Id, SelectedTour.Id);
                    attendanceService.Save(tourAttendance);

                    if (SelectedVoucher != null)
                    {
                        voucherService.Delete(SelectedVoucher.Id);
                        Vouchers.Remove(SelectedVoucher);
                    }

                    MessageBox.Show("Reservation is successful");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number\n" +
                                "in order to make a reservation!");
            }
        }
    }
}
