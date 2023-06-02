using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.GuestOne;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestOneHomeViewModel
    {

        public ICommand Search_Command { get; set; }

        public GuestOneHomeViewModel(User user)
        {
            Search_Command = new RelayCommand(SearchAccommodations, CanSearch);
        }

        private bool CanSearch()
        {
            return true;
        }

        private void SearchAccommodations()
        {
            var searchView = new GuestOneSearchWindow();
            searchView.Show();
        }

    }
}
