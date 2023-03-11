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
using Tourist_Project.Controller;
using Tourist_Project.Model;
using Image = Tourist_Project.Model.Image;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private TourController tourController { get; set; }
        private LocationController locationController { get; set; }
        private ImageController imageController { get; set; }
        public CreateTour()
        {
            InitializeComponent();
        }

        public void Create(object sender, RoutedEventArgs e)
        {
            Location location = new Location(locationController.GetId(City.Text, Country.Text), City.Text, Country.Text);

            Image image = new Image(imageController.GetId(Url.Text), Url.Text);
            Tour tour = new Tour();

            tour.Location = location;
            tour.LocationId = location.Id;
            tour.Description = Description.Text;
            tour.Language = Language.Text;
            tour.MaxGuestsNumber = Convert.ToInt32(MaxGuestsNumber.Text);
            tour.StartTime = Convert.ToDateTime(StartTime.Text);
            tour.Duration = Convert.ToInt32(Duration.Text);
            tour.Image = image; 
            tourController.Create(tour);
            this.Close();
        }
        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
