using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestTwoViewModel : ViewModelBase
    {
        public User LoggedUser { get; set; }
        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;
        private readonly NavigationStore navigationStore;
        private readonly VoucherService voucherService;
        public ICommand HomeCommand { get; set; }
        public ICommand MyToursCommand { get; set; }
        public ICommand TourHistoryCommand { get; set; }
        public ICommand VouchersCommand { get; set; }

        public GuestTwoViewModel(User user, NavigationStore navigationStore)
        {
            LoggedUser = user;
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            voucherService = new VoucherService();

            HomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(user, navigationStore));
            MyToursCommand = new NavigateCommand<MyToursViewModel>(navigationStore, () => new MyToursViewModel(user, navigationStore));
            TourHistoryCommand = new NavigateCommand<TourHistoryViewModel>(navigationStore, () => new TourHistoryViewModel(user, navigationStore));
            VouchersCommand = new NavigateCommand<VouchersViewModel>(navigationStore, () => new VouchersViewModel(user));

            voucherService.DeleteInvalidVouchers(LoggedUser.Id);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
