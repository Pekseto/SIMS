using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class CancelRescheduleViewModel
    {
        public ReschedulingReservationViewModel RescheduleRequestViewModel { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public CancelRescheduleRequest Window { get; set; }
        private readonly RescheduleRequestService rescheduleRequestService = new();
        public OwnerMainWindowViewModel ownerMainWindowViewModel;

        public CancelRescheduleViewModel(CancelRescheduleRequest window, OwnerMainWindowViewModel ownerMainWindowViewModel, ReschedulingReservationViewModel reschedulingReservationViewModel)
        {
            RescheduleRequestViewModel = reschedulingReservationViewModel;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            this.ownerMainWindowViewModel = ownerMainWindowViewModel;
            Window = window;
        }
        #region Commands
        public void Confirm()
        {
            RescheduleRequestViewModel.RescheduleRequest.Comment = Window.Comment.Text;
            RescheduleRequestViewModel.RescheduleRequest.Status = RequestStatus.Declined;
            rescheduleRequestService.Update(RescheduleRequestViewModel.RescheduleRequest);
            ownerMainWindowViewModel.RescheduleRequestUpdate(RescheduleRequestViewModel);
            Window.Close();
        }

        public void Cancel()
        {
            Window.Close();
        }
        #endregion
    }

}