using System;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.WPF.Views;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.ViewModels
{
    public class RescheduleReservationViewModel
    {
        private Window _window;
        private RescheduleRequestService _rescheduleRequestService = new();
        private RescheduleRequestRepository _rescheduleRequestRepository = new();

        public DateTime NewBegginigDate { get; set; }
        public DateTime NewEndDate { get; set; }

        public int NewStayingDays { get; set; }

        public ICommand ConfirmRescheduling_Command { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public Reservation SelectedReservation { get; set; } = new();

        public String Comment { get; set; }

        public int Id { get; set; }

        public RescheduleReservationViewModel(Window window, Reservation selectedReservation)
        {
            this._window = window;
            SelectedReservation = selectedReservation;
            ConfirmRescheduling_Command = new RelayCommand(RescheduleReservation, CanReschedule);
            RequestStatus = RequestStatus.Pending;
            Id = _rescheduleRequestRepository.NextId();
            Comment = String.Empty;
        }

        private bool CanReschedule()
        {
            if (IsInputValid())
                return true;
            else
                return false;
        }

        private bool IsInputValid()
        {
            if (NewBegginigDate != DateTime.MinValue && NewEndDate != DateTime.MinValue && NewBegginigDate.Date < NewEndDate.Date)
                return true;
            else
                return true;
        }

        private void RescheduleReservation()
        {
            RescheduleRequest rescheduleRequest = new RescheduleRequest(SelectedReservation.CheckIn, SelectedReservation.CheckOut, NewBegginigDate, NewEndDate, Id, SelectedReservation.Id, RequestStatus, Comment);
            _rescheduleRequestService.Create(rescheduleRequest);
            MessageBox.Show("Reschedule Request Created");
        }

        //private List<>

    }
}
