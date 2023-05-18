using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class RateGuestViewModel : INotifyPropertyChanged
    {
        private Notification notification;
        public Notification Notification
        {
            get => notification;
            set
            {
                if(value == notification) return;
                notification = value;
                OnPropertyChanged("Notification");
            }
        }

        private GuestRateViewModel guestRate;
        public GuestRateViewModel GuestRate
        {
            get => guestRate;
            set
            {
                if(value == guestRate) return;
                guestRate = value;
                OnPropertyChanged("GuestRate");
            }
        }
        private readonly GuestRateService ratingService = new ();
        private readonly NotificationService notificationService = new ();
        public ICommand RateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Window Window { get; set; }
        public OwnerMainWindowViewModel OwnerMainWindowViewModel;
        public RateGuestViewModel(Notification notification, Window window, OwnerMainWindowViewModel ownerMainWindowViewModel)
        {
            Notification = notification;
            OwnerMainWindowViewModel = ownerMainWindowViewModel;
            GuestRate = new GuestRateViewModel(notification);
            RateCommand = new RelayCommand(Rate, CanRate);
            CancelCommand = new RelayCommand(Cancel);
            Window = window;
        }
        #region Commands
        public void Rate()
        {
            notificationService.Delete(Notification.Id);
            ratingService.Update(GuestRate.GuestRating);
            OwnerMainWindowViewModel.GuestRateUpdate(Notification);
            Window.Close();
        }

        public bool CanRate()
        {
            return true;
        }

        public void Cancel()
        {
            Window.Close();
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}