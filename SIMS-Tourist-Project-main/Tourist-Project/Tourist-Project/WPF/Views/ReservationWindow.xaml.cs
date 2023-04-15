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
using Tourist_Project.View;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using System.Collections.ObjectModel;
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {

        public String AccommodationName { get; set; }
        public String Location { get; set; }
        public String Type { get; set; }

        public DateTime ReservationBegins { get; set; }
        public DateTime ReservationEnds { get; set; }

        public String StayingDays { get; set; }
        public List<DateTime> AvailableDates { get; set; }

        public int AccommodationId { get; set; }

        public List<AccommodationDTO> AccommodationDTOs { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        private AccommodationDtoRepository _accommodationDTORepository { get; }
        public ReservationWindow(Accommodation SelectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = new CreateReservationViewModel(this, SelectedAccommodation);

            //AccommodationName = SelectedAccommodationDTO.Name;
            //Location = SelectedAccommodationDTO.LocationFullName;
            //Type = SelectedAccommodationDTO.AccommodationType.ToString();
            //AvailableDates = SelectedAccommodationDTO.AvailableDates;
            //AccommodationId = SelectedAccommodationDTO.AccommodationId;
           // AccommodationDTOs = AccommodationDtoRepository.

        }

        /*private String GetName(AccommodationDTO SelectedAccommodationDTO)
        {
            return SelectedAccommodationDTO.Name;
        }
        
        private String GetLocation(AccommodationDTO SelectedAccommodationDTO)
        {
            return SelectedAccommodationDTO.LocationFullName;
        }
        private String GetType(AccommodationDTO SelectedAccommodationDTO)
        {
            if (SelectedAccommodationDTO.AccommodationType.ToString().Equals("Apartment"))
            {
                return "Apartment";
            }
            else if (SelectedAccommodationDTO.AccommodationType.ToString().Equals("House"))
            {
                return "House";
            }
            else if (SelectedAccommodationDTO.AccommodationType.ToString().Equals("Cottage"))
            {
                return "Cottage";
            }
            else
            {
                return "Not a Valid Type";
            }
            
        }*/



        /*public AccommodationDTO FindAccommodation()
        {
            //return 
        }*/


        public bool ReservationLogic(DateTime ReservationBegins, DateTime ReservationEnds, String StayingDays)
        {
            int DaysAvailable = 0;

            foreach(DateTime dt in AvailableDates)//prvo pronaci smestaj po id-u, pa odraditi logiku
            {
                if(ReservationBegins > dt && ReservationEnds < dt)
                {
                    DaysAvailable++;
                    if (DaysAvailable == Convert.ToInt32(StayingDays))
                    {
                        DaysAvailable = 0;
                        return true;
                    }   
                }
            }
               return false;

        }


        public void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(ReservationLogic(ReservationBegins, ReservationEnds, StayingDays))
            {
                for(DateTime TakenDays = ReservationBegins; TakenDays <= ReservationEnds; TakenDays.AddDays(1))
                {
                    AvailableDates.Remove(TakenDays);
                    //MessageBox
                }
            }
        }

        public void Home_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}