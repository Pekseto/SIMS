using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestTwoViewModel : INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }

        private object currentViewModel;
        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set 
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                
            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand MyToursCommand { get; set; }
        public ICommand TourHistoryCommand { get; set; }

        public GuestTwoViewModel(User user)
        {
            LoggedUser = user;
            CurrentViewModel = new HomeViewModel(user, CurrentViewModel);

            HomeCommand = new RelayCommand(OnHomeClick);
            MyToursCommand = new RelayCommand(OnMyToursClick);
            TourHistoryCommand = new RelayCommand(OnTourHistoryClick);
        }

        private void OnTourHistoryClick()
        {
            CurrentViewModel = new TourHistoryViewModel(LoggedUser, CurrentViewModel);
        }

        private void OnMyToursClick()
        {
            CurrentViewModel = new MyToursViewModel(LoggedUser);
        }

        private void OnHomeClick()
        {
            CurrentViewModel = new HomeViewModel(LoggedUser, CurrentViewModel);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
