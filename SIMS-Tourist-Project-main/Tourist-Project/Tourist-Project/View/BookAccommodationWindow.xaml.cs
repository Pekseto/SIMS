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
using Tourist_Project.DTO;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for BookAccommodationWindow.xaml
    /// </summary>
    public partial class BookAccommodationWindow : Window
    {

        public string FullLocationName { get; set; }
        public string AccommodationType { get; set; }   
        public int MaxGuestsNumber { get; set; }
        public int MinStayingDays { get; set; }
        public int DaysBeforeCancelation { get; set; }
        
        public AccommodationDTO SelectedAccommodationDTO { get; set; }  
        public BookAccommodationWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            FullLocationName = SelectedAccommodationDTO.LocationFullName;
            AccommodationType = SelectedAccommodationDTO.AccommodationType.ToString();

        }
    }
}
