using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;
using Tourist_Project.WPF.Views;
using Tourist_Project.WPF.Views.GuestTwo;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestTwoViewModel : ViewModelBase
    {
        private GuestTwoView guestTwoWindow;
        public User LoggedUser { get; set; }
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;
        private readonly NavigationStore navigationStore;
        private readonly VoucherService voucherService = new();
        public ICommand HomeCommand { get; set; }
        public ICommand MyToursCommand { get; set; }
        public ICommand TourHistoryCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand SignOutCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand NotificationsCommand { get; set; }

        public GuestTwoViewModel(User user, NavigationStore navigationStore, GuestTwoView guestTwoWindow)
        {
            LoggedUser = user;
            this.navigationStore = navigationStore;
            this.guestTwoWindow = guestTwoWindow;
            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            HomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(user, navigationStore));
            MyToursCommand = new NavigateCommand<MyToursViewModel>(navigationStore, () => new MyToursViewModel(user, navigationStore));
            TourHistoryCommand = new NavigateCommand<TourHistoryViewModel>(navigationStore, () => new TourHistoryViewModel(user, navigationStore));
            VouchersCommand = new NavigateCommand<VouchersViewModel>(navigationStore, () => new VouchersViewModel(user));
            NotificationsCommand = new NavigateCommand<NotificationsViewModel>(navigationStore, () => new NotificationsViewModel(user, navigationStore));
            SignOutCommand = new RelayCommand(OnSignOutClick);
            ExitCommand = new RelayCommand(OnExitClick);

            voucherService.DeleteInvalidVouchers(LoggedUser.Id);
        }

        private void OnExitClick()
        {
            guestTwoWindow.Close();
        }

        private void OnSignOutClick()
        {
            var LoginWindow = new LoginWindow();
            LoginWindow.Show();
            guestTwoWindow.Close();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
