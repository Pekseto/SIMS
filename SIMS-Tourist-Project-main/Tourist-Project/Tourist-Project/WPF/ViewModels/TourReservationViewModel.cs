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
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReservationViewModel : ViewModelBase
    {
        private readonly TourVoucherService voucherService = new();
        private readonly TourReservationService reservationService = new();
        private readonly TourAttendanceService attendanceService = new();
        private readonly TourPointRepository tourPointRepository = new();
        private readonly ImageRepository imageRepository = new();
        public int GuestsNumber { get; set; }
        public ObservableCollection<TourVoucher> Vouchers { get; set; }
        private TourVoucher selectedVoucher;
        public TourVoucher SelectedVoucher
        {
            get => selectedVoucher;
            set
            {
                if(value != selectedVoucher)
                {
                    selectedVoucher = value;
                    OnPropertyChanged(nameof(SelectedVoucher));
                }
            }
        }
        public User LoggedUser { get; set; }
        private readonly NavigationStore navigationStore;
        public TourDTO SelectedTour { get; set; }
        public string Checkpoints { get; set; }

        private Message message;
        public Message Message
        {
            get => message;
            set
            {
                if (value == message) return;
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public List<Image> TourImages { get; set; }
        private int imageId;
        private readonly int imagesCount;
        private Image currentImage;
        public Image CurrentImage
        {
            get => currentImage;
            set
            {
                if (value != currentImage)
                {
                    currentImage = value;
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand ReserveCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public TourReservationViewModel(User user, TourDTO tour, NavigationStore navigationStore, HomeViewModel previousViewModel)
        {
            LoggedUser = user;
            SelectedTour = tour;
            this.navigationStore = navigationStore;

            TourImages = imageRepository.GetByAssociationAndId(ImageAssociation.Tour, tour.Id);
            imagesCount = TourImages.Count;
            if (imagesCount > 0)
            {
                CurrentImage = TourImages[0];
                imageId = 0;
            }
            else
            {
                CurrentImage = new Image("/Images/No images to show.jpg");
            }


            Checkpoints = tourPointRepository.GetAllForTourString(SelectedTour.Id);
            Vouchers = LoadVouchers(); //Prebaciti u servis
            SelectedVoucher = Vouchers.First();

            ReserveCommand = new RelayCommand(OnReserveClick, CanReserve);
            BackCommand = new NavigateCommand<HomeViewModel>(this.navigationStore, () => previousViewModel);
            NextCommand = new RelayCommand(OnNextClick, () => imagesCount > 0);
            PreviousCommand = new RelayCommand(OnPreviousClick, () => imagesCount > 0);
        }

        public TourReservationViewModel(User user, TourDTO tour, NavigationStore navigationStore, SimilarToursViewModel previousViewModel)
        {
            LoggedUser = user;
            SelectedTour = tour;
            this.navigationStore = navigationStore;

            TourImages = imageRepository.GetByAssociationAndId(ImageAssociation.Tour, tour.Id);
            CurrentImage = TourImages[0];
            imagesCount = TourImages.Count;
            imageId = 0;

            Checkpoints = tourPointRepository.GetAllForTourString(SelectedTour.Id);
            Vouchers = LoadVouchers();
            SelectedVoucher = Vouchers.First();

            ReserveCommand = new RelayCommand(OnReserveClick, CanReserve);
            BackCommand = new NavigateCommand<SimilarToursViewModel>(this.navigationStore, () => previousViewModel);
            NextCommand = new RelayCommand(OnNextClick);
            PreviousCommand = new RelayCommand(OnPreviousClick);
        }

        private void OnPreviousClick()
        {
            imageId = imageId - 1 < 0 ? imagesCount - 1 : imageId - 1;
            CurrentImage = TourImages[imageId];
        }

        private void OnNextClick()
        {
            imageId = imageId + 1 == imagesCount ? 0 : imageId + 1;
            CurrentImage = TourImages[imageId];
        }

        private ObservableCollection<TourVoucher> LoadVouchers()
        {
            var retVal = new ObservableCollection<TourVoucher>{ new TourVoucher() };
            foreach (var voucher in voucherService.GetAllForUser(LoggedUser.Id))
            {
                retVal.Add(voucher);
            }
            return retVal;
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
            var tourCapacityLeft = SelectedTour.SpotsLeft;

            if (tourCapacityLeft == 0)
            {
                navigationStore.CurrentViewModel = new SimilarToursViewModel(LoggedUser, SelectedTour.LocationId, SelectedTour.Id, navigationStore, this);
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
                if (SelectedVoucher.Name != "Without voucher")
                {
                    voucherService.Delete(SelectedVoucher.Id);
                    Vouchers.Remove(SelectedVoucher);
                    if (Vouchers.Count > 0)
                    {
                        SelectedVoucher = Vouchers.First();
                    }
                }

                _ = ShowMessageAndHide(new Message(true, "Successful reservation"));
            }

        }
    }
}
