using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Windows.Input;
using System.Windows.Controls;
using Tourist_Project.WPF.Views;
using System.ComponentModel;
using Tourist_Project.WPF.Validation;

namespace Tourist_Project.WPF.ViewModels
{
    public class CreateReservationViewModel : INotifyPropertyChanged
    {

        private Window _window;
        private User _user;
        private ReservationService _reservationService { get; set; }
        private GuestService _guestService { get; set; }
        public List<Reservation> ReservationsForSelectedAccommodation { get; set; }

        public String Name { get; set; }

        private int _stayingDays;
        public int StayingDays
        {
            get { return _stayingDays; }
            set
            {
                _stayingDays = value;
                OnPropertyChanged(nameof(StayingDays));
            }
        }
        public int MinStayingDays { get; set; }

        public int GuestNum { get; set; }

        private int _searchedGuestNum;
        public int SearchedGuestNum
        {
            get { return _searchedGuestNum; }
            set
            {
                _searchedGuestNum = value;
                OnPropertyChanged(nameof(SearchedGuestNum));
            }
        }
        public String Type { get; set; }

        private DateTime _from;
        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
            
        }

        private DateTime _to;
        public DateTime To
        {
            get { return _to; }

            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
           
        }
        public List<DateTime> AvailableDates { get; set; }
        public ICommand SeeAvailableReservations_Command { get; set; }

        public ICommand Home_Command { get; set; }
        public Accommodation SelectedAccommodation { get; set; } = new();

        public bool CanCreateRes { get; set; }

        public bool AreStayingDaysValid { get; set; }

        public bool IsGuestNumValid { get; set; }

        public bool AreDatesValid { get; set; }
        //[StayingDaysValidation(nameof(MinStayingDays))]
        public CreateReservationViewModel(Window _window, Accommodation selectedAccommodation, User user)
        {
            this._window = _window;
            _user = user;
            _reservationService = new ReservationService();
            Name = selectedAccommodation.Name;
            Type = selectedAccommodation.Type.ToString();
            MinStayingDays = selectedAccommodation.MinStayingDays;
            SelectedAccommodation = selectedAccommodation;
            GuestNum = selectedAccommodation.MaxGuestNum;
            SetDefaultParameters();
            SeeAvailableReservations_Command = new RelayCommand(ShowAvailableReservations, CanCreate);
            Home_Command = new RelayCommand(HomeCommand, CanHome);
            //ReservationsForSelectedAccommodation = new List<Reservation>();
            //ReservationsForSelectedAccommodation = _reservationService.FindReservationsForAccommodation(SelectedAccommodation);
            
            SelectedAccommodation = selectedAccommodation;
        }

        private bool CanHome()
        {
            return true;
        }

        private void HomeCommand()
        {
            _window.Close();
        }

        public void SetDefaultParameters()
        {
            From = DateTime.Now;
            To = DateTime.Now.AddDays(SelectedAccommodation.MinStayingDays);
            SearchedGuestNum = SelectedAccommodation.MaxGuestNum;
            StayingDays = SelectedAccommodation.MinStayingDays;
            CanCreateRes = true;
            IsGuestNumValid = true;
            AreStayingDaysValid = true;
            AreDatesValid = true;
        }
        public List<DateTime> GenerateFreeDates()
        {
            DateTime dt = DateTime.Now;
            dt.AddMonths(2);
            AvailableDates.Add(dt);
            return AvailableDates;
        }
        private bool CanCreate()
        {
            //Treba samo bool da dobije na osnovu logike za validaciju
            return CanCreateRes;
        }

        private void GuestNumValidation()
        {
            {
                if (SearchedGuestNum > GuestNum)
                {
                    //MessageBox.Show("Number of guests for this accommodation is too big");    
                    IsGuestNumValid = false;
                    CanCreateRes = false;
                }
                else
                    IsGuestNumValid = true;
            }
        }

        private void StayingDaysValidation()
        {
            if (StayingDays < MinStayingDays)
            {
                //MessageBox.Show("Too small number of staying days");
                AreStayingDaysValid = false;
                CanCreateRes = false;
            }
            else
                AreStayingDaysValid = true;
                
        }

        private void DateValidation()
        {
            if (To.Date < From.Date)
            {
                //MessageBox.Show("Ending date has to be greater than beginnig date");
                AreDatesValid = false;
                CanCreateRes = false;
            }
            else
                AreDatesValid = true;
           
                
        }

        private void ResetCanCreateRes()
        {
            if (AreStayingDaysValid && IsGuestNumValid && AreDatesValid)
                CanCreateRes = true;
            else
                CanCreateRes = false;
        }
        public void ShowAvailableReservations()
        {
            GuestNumValidation();
            StayingDaysValidation();
            DateValidation();
            ResetCanCreateRes();
            if (CanCreateRes)
            {
                var availableReservationsWindow = new AvailableReservationsWindow(SelectedAccommodation, From, To, StayingDays, SearchedGuestNum, _user);
                availableReservationsWindow.Show();
            }
            else
            {
                if(!IsGuestNumValid)
                {
                    MessageBox.Show("Number of guests for this accommodation is too big");
                    CanCreateRes = true;
                }
                else if(!AreStayingDaysValid)
                {
                    MessageBox.Show("Too small number of staying days");
                    CanCreateRes = true;
                }
                else if(!AreDatesValid)
                {
                    MessageBox.Show("Ending date has to be greater than beginnig date");
                    CanCreateRes = true;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //ideja je da on ima 2 meseca, odabere od kad do kad hoce smestaj i onda da mu otvorim novi prozor koji mu prikazuje sve raspone

    }
}
