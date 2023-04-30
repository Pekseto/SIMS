using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class CancelRescheduleViewModel
    {
        public RescheduleRequest RescheduleRequest { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public CancelRescheduleRequest Window { get; set; }
        private readonly RescheduleRequestService rescheduleRequestService = new();

        public CancelRescheduleViewModel(RescheduleRequest rescheduleRequest, CancelRescheduleRequest window)
        {
            RescheduleRequest = rescheduleRequest;
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            Window = window;
        }
        #region Commands
        public void Confirm()
        {
            RescheduleRequest.Comment = Window.Comment.Text;
            RescheduleRequest.Status = RequestStatus.Declined;
            rescheduleRequestService.Update(RescheduleRequest);
            Window.Close();
        }
        public static bool CanConfirm()
        {
            return true;
        } 
        #endregion
    }

}