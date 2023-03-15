using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for TourLiveWindow.xaml
    /// </summary>
    public partial class TourLiveWindow : Window
    {
        public static ObservableCollection<TourPoint> TourPoints { get; set; }
        public TourPoint SelectedTourPoint { get; set; }
        private readonly TourPointRepository tourPointRepository;

        public TourLiveWindow(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            tourPointRepository = new TourPointRepository();
            TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(selectedTour.Id));
        }
    }
}
