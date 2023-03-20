using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Repository;
using Tourist_Project.Model;
using System.Collections.ObjectModel;
using Tourist_Project.DTO;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for BookAccommodationWindow.xaml
    /// </summary>
    public partial class BookAccommodationWindow : Window
    {

        private readonly ReservationRepository reservationRepository;
        public Reservation Reservation { get; set; }
        
        public ObservableCollection<Reservation> ReservationsAvailable { get; set; }

        public ObservableCollection<Reservation> TakenReservations { get; set; }

        public ObservableCollection<DateTime> AvailableDates { get; set; }    

        //public ObservableCollection<DateTime> UnavailableDates { get; set; }
        public int MinStayingDays { get; set; }
        public int MaxGuestNum { get; set; }

        public int DaysBeforeCancelation { get; set; }
        public DateTime CheckInInput { get; set; }
        public DateTime CheckOutInput { get; set; }
        public BookAccommodationWindow(AccommodationDTO SelectedAccommodationDTO)
        {
            InitializeComponent();
            this.DataContext = this;

            reservationRepository = new ReservationRepository();
            TakenReservations = new ObservableCollection<Reservation>();

            ReservationsAvailable = new ObservableCollection<Reservation>(reservationRepository.GetAll());
            AvailableDates = new ObservableCollection<DateTime>();
            //UnavailableDates = new ObservableCollection<DateTime>();

            GetAvailableDates();

        }

        public void GetAvailableDates()
        {
            foreach (Reservation reservation in ReservationsAvailable)
            {
                AvailableDates.Add(reservation.CheckIn);
                AvailableDates.Add(reservation.CheckOut);
            }
        }

        public void CheckConditions(AccommodationDTO SelectedAccommodationDTO)
        {
            if(SelectedAccommodationDTO.MaxGuestNum < MaxGuestNum)
            {
                MessageBox.Show("Number of guests is too high for this accommodation");
                return;
                
            }
            if(SelectedAccommodationDTO.DaysBeforeCancel < DaysBeforeCancelation)
            {
                MessageBox.Show("Number of days before cancelation is too small");
                return;
                
            }
            if(SelectedAccommodationDTO.MinStayingDays < MinStayingDays)
            {
                MessageBox.Show("Too few days for staying");
                return;
            }
         
                 

        }

        private void ConfirmReservation(object sender, RoutedEventArgs e)
        {
            if (AvailableDates.Contains(CheckInInput) && AvailableDates.Contains(CheckOutInput))
            {
                foreach (DateTime dt in AvailableDates)
                {
                    if (dt >= CheckInInput && dt <= CheckOutInput)
                    {
                        return;
                    }
                    AvailableDates.Remove(dt);
                }

                MessageBox.Show("Accommodation reserved");
            }
        }



    }
}
