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
using Tourist_Project.Repository;
using Image = Tourist_Project.Model.Image;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private TourRepository tourRepository;
        private LocationRepository locationRepository;
        private ImageController imageController;
        private TourPointRepository tourPointRepository;
        private Tour tour;
        public CreateTour()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            imageController = new ImageController();
            tour = new Tour();
        }

        public void CreateClick(object sender, RoutedEventArgs e)
        {
            Image image = new Image(Url.Text);
            //imageRepository.Save(Image);

            //Proveriti prilikom dodavanja
            tour = new Tour(Name.Text, locationRepository.GetId(City.Text, Country.Text), Description.Text, Language.Text, Convert.ToInt32(MaxGuestsNumber.Text), Convert.ToDateTime(StartTime.Text), Convert.ToInt32(Duration.Text), image.Id);
            tourRepository.Save(tour);
            this.Close();
        }
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void AddCheckpointClick(object sender, RoutedEventArgs e)
        {
            TourPoint tourPoint = new TourPoint(Checkpoint.Text, tourRepository.NextId());
            tourPointRepository.Save(tourPoint);

            if (!string.IsNullOrWhiteSpace(Checkpoint.Text))
            {
                tour.TourPoints.Add(tourPoint);
                
            }
        }
    }
}
