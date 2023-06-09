using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestOneSearchResultsViewModel
    {
        private Window _window { get; set; }
        public ICommand Close_Command;
        public DataGrid GuestOneSearchResultsDataGrid { get; } = new();

        public GuestOneSearchResultsViewModel(Window window, ObservableCollection<AccommodationViewModel> searchResults)
        {
            this._window = window;
            Close_Command = new RelayCommand(Close, CanClose);
            GuestOneSearchResultsDataGrid.DataContext = searchResults;

        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            this.Close();
        }
    }
}
