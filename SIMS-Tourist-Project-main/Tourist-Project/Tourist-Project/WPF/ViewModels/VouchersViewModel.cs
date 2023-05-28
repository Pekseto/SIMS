using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class VouchersViewModel : ViewModelBase
    {
        private readonly TourVoucherService voucherService = new();
        public ObservableCollection<TourVoucher> Vouchers { get; set; }

        public VouchersViewModel(User user)
        {
            Vouchers = new ObservableCollection<TourVoucher>(voucherService.GetAllForUser(user.Id));
        }
    }
}
