using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class RescheduleReservationViewModel
    {
        private Window _window; 

        public DateTime NewBegginigDate { get; set; }
        public DateTime NewEndDate { get; set; }

        public int NewStayingDays { get; set; }

        public ICommand ConfirmRescheduling_Command { get; set; }

        public RescheduleReservationViewModel(Window window)
        {
            this._window = window;
           // ConfirmRescheduling_Command = new RelayCommand(, CanReschedule);

        }

        private bool CanReschedule()
        {
            if (NewBegginigDate != DateTime.MinValue && NewEndDate != DateTime.MinValue)
                return true;
            else
                return false;
        }

        //private List<>

    }
}
