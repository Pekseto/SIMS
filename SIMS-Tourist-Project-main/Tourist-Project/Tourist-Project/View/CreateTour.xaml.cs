using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;
using Tourist_Project.Model;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private TourController tourController { get; set; }
        public CreateTour()
        {
            InitializeComponent();
        }

        public void Create()
        {
            
            Tour tour = new Tour();

            tour.Location = location;
            tour.LocationId = location.Id; //TODO
            tour.Description = Description.Text;
            tour.Language = Language.Text;
            tour.MaxGuestsNumber = Convert.ToInt32(MaxGuestsNumber.Text);
            tour.StartTime = Convert.ToDateTime(StartTime.Text);
            tour.Duration = Convert.ToInt32(Duration.Text);
            tour.Image = image;
            tourController.Create(tour);
            this.Close();
        }
    }
}
