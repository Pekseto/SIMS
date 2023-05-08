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
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReservationViewModel : ViewModelBase
    {
        private readonly VoucherService voucherService = new();
        private readonly TourReservationService reservationService = new();
        private readonly TourAttendanceService attendanceService = new();
        private readonly TourPointRepository tourPointRepository = new();
        public int GuestsNumber { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public User LoggedUser { get; set; }
        private readonly NavigationStore navigationStore;
        public TourDTO SelectedTour { get; set; }
        public string Checkpoints { get; set; }

        private Message message;
        public Message Message
        {
            get { return message; }
            set
            {
                if (value == message) return;
                message = value;
                OnPropertyChanged();
            }
        }
        public ICommand ReserveCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public TourReservationViewModel(User user, TourDTO tour, NavigationStore navigationStore, HomeViewModel previousViewModel)
        {
            LoggedUser = user;
            SelectedTour = tour;
            this.navigationStore = navigationStore;

            ReserveCommand = new RelayCommand(OnReserveClick, CanReserve);
            BackCommand = new NavigateCommand<HomeViewModel>(this.navigationStore, () => previousViewModel);

            Checkpoints = tourPointRepository.GetAllForTourString(SelectedTour.Id);
            Vouchers = new ObservableCollection<Voucher>(voucherService.GetAllForUser(user.Id));
        }

        private bool CanReserve()
        {
            return GuestsNumber > 0;
        }

        private async Task ShowMessageAndHide(Message message)
        {
            Message = message;
            await Task.Delay(5000);
            Message = new Message();
        }

        private void OnReserveClick()
        {
            int tourCapacityLeft = SelectedTour.SpotsLeft;

            if (tourCapacityLeft == 0)
            {
                _ = ShowMessageAndHide(new Message(false, "This tours capacity is full at the moment.\n" +
                                                          "Here are some other tours on the same location!"));
                //DisplaySimilarTours(SelectedTour);     DODATI OVU FUNKCIONALNOST
            }
            else if (tourCapacityLeft < GuestsNumber)
            {
                _ = ShowMessageAndHide(new Message(false, "Unfortunately, we can't accept that many guests.\n" +
                                                          "Capacity left: " + tourCapacityLeft.ToString()));
            }
            else
            {
                var tourReservation = reservationService.GetByUserIdAndTourId(LoggedUser.Id, SelectedTour.Id);
                if(tourReservation == null)
                {
                    var newReservation = new TourReservation(LoggedUser.Id, SelectedTour.Id, GuestsNumber, SelectedVoucher != null ? true : false);
                    reservationService.Save(newReservation);
                    var newAttendance = new TourAttendance(LoggedUser.Id, SelectedTour.Id);
                    attendanceService.Save(newAttendance);
                }
                else
                {
                    tourReservation.GuestsNumber += GuestsNumber;
                    reservationService.Update(tourReservation);
                }


                SelectedTour.SpotsLeft -= GuestsNumber;
                if (SelectedVoucher != null)
                {
                    voucherService.Delete(SelectedVoucher.Id);
                    Vouchers.Remove(SelectedVoucher);
                }

                _ = ShowMessageAndHide(new Message(true, "Successful reservation"));
            }

        }
    }
}
