using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class CancelRescheduleViewModel
    {
        public RescheduleRequest RescheduleRequest { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public CancelRescheduleRequest window { get; set; }
        private readonly RescheduleRequestService _service = new();

        public CancelRescheduleViewModel(RescheduleRequest rescheduleRequest, CancelRescheduleRequest window)
        {
            RescheduleRequest = rescheduleRequest;
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            this.window = window;
        }
        public void Confirm()
        {
            RescheduleRequest.Comment = window.Comment.Text;
            RescheduleRequest.Status = RequestStatus.Declined;
            _service.Update(RescheduleRequest);
            window.Close();
        }
        public static bool CanConfirm()
        {
            return true;
        }
    }

}