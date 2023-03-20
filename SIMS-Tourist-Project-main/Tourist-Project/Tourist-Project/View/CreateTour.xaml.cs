using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        private TourPointRepository tourPointRepository;
        private ImageRepository imageRepository;
        private Tour tour;

        public CreateTour()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            locationRepository = new LocationRepository();
            tourPointRepository = new TourPointRepository();
            imageRepository = new ImageRepository();
            tour = new Tour();
        }

        public void CreateClick(object sender, RoutedEventArgs e)
        {
            if (tour.TourPoints.Count() >= 2)
            {
                Image image = new Image(url.Text);
                imageRepository.Save(image);
                tour = new Tour(Name.Text, locationRepository.GetId(City.Text, Country.Text), description.Text, language.Text, Convert.ToInt32(maxGuestsNumber.Text), Convert.ToDateTime(startTime.Text), Convert.ToInt32(duration.Text), image.Id);
                tourRepository.Save(tour);

                if (tour.StartTime.Date == DateTime.Today.Date)
                {
                    GuideShowWindow.TodayTours.Add(tour);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("You must enter minimum two checkpoints!");
            }
            
        }
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddCheckpointClick(object sender, RoutedEventArgs e)
        {
            TourPoint tourPoint = new TourPoint(checkpoint.Text, tourRepository.NextId());
            tourPointRepository.Save(tourPoint);

            if (!string.IsNullOrWhiteSpace(checkpoint.Text))
            {
                tour.TourPoints.Add(tourPoint);
                checkpoint.Clear();
            }
        }

        public void AddImageClick(object sender, RoutedEventArgs e)
        {
            Image image = new Image(url.Text);
            imageRepository.Save(image);

            if (!string.IsNullOrWhiteSpace(url.Text))
            {
                url.Clear();
            }
        }
    }
}
