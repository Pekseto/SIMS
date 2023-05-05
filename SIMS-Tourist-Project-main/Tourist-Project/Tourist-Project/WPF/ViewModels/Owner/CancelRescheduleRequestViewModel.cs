using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class CancelRescheduleViewModel
    {
        public ICommand ConfirmCommand { get; set; }
        public CancelRescheduleRequest Window { get; set; }
        private readonly RescheduleRequestService rescheduleRequestService = new();

        public CancelRescheduleViewModel(CancelRescheduleRequest window)
        {
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            Window = window;
        }
        #region Commands
        public void Confirm()
        {
            Window.Close();
        }
        public static bool CanConfirm()
        {
            return true;
        } 
        #endregion
    }

}