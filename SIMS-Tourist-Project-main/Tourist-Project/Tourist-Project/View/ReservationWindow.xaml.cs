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

        public ReservationWindow(AccommodationDTO SelectedAccommodationDTO)
        {
            InitializeComponent();
            this.DataContext = this;
            
            AccommodationName = SelectedAccommodationDTO.Name;
            Location =SelectedAccommodationDTO.LocationFullName;
            Type = SelectedAccommodationDTO.AccommodationType.ToString();
           
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


        public bool ReservationLogic(ReservationBegins, ReservationEnds)
        {

        }


        public void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Home_Click(object sender, RoutedEventArgs e) 
        {
            
        }



    }
}
