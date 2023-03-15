using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuideShowWindow.xaml
    /// </summary>
    public partial class GuideShowWindow : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourRepository tourRepository;

        public GuideShowWindow()
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            Tours = new ObservableCollection<Tour>(tourRepository.GetAll().FindAll(tour => tour.StartTime == DateTime.Now)); //TODO : Clean code
            //All tours during day
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var createTourWindow = new CreateTour();
            createTourWindow.Show();
        }

        private void StartTourClick(object sender, RoutedEventArgs e)
        {
            var tourLiveWindow = new TourLiveWindow(SelectedTour);
            tourLiveWindow.ShowDialog();
        }
    }
}
