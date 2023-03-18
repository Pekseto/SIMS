using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public TourPoint SelectedTourPoint { get; set; }
        private Tour selectedTour;
        private TourPointRepository tourPointRepository;

        public TourLiveWindow(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            tourPointRepository = new TourPointRepository();
            this.selectedTour = selectedTour;
            TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(this.selectedTour.Id));
            TourPoints[0].Visited = true;
            tourPointRepository.Update(TourPoints[0]);
        }

        public void EarlyEndClick(object sender, RoutedEventArgs e)
        {
            selectedTour.Guided = true;
            GuideShowWindow.Live = false;
            Close();
        }

        public void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            SelectedTourPoint.Visited = true;
            tourPointRepository.Update(SelectedTourPoint);
            TourPoints.Clear();
            foreach(TourPoint point in tourPointRepository.GetAllForTour(selectedTour.Id))
            {
                TourPoints.Add(point);
            }
            //TourPoints = new ObservableCollection<TourPoint>(tourPointRepository.GetAllForTour(selectedTour.Id));
            //int index = TourPoints.IndexOf(SelectedTourPoint);
            //TourPoints.Remove(SelectedTourPoint);
            //TourPoints.Insert(index, SelectedTourPoint);
            if (IsAllChecked(TourPoints))
            {
                selectedTour.Guided = true;
                GuideShowWindow.Live = false;
                MessageBox.Show("Tour ended");
                Close();

            }
            //TourPoints.Clear();
        }

        public bool IsAllChecked(ObservableCollection<TourPoint> tourPoints)
        {
            return tourPoints.ToList().Find(tourPoint => tourPoint.Visited == false) == null;
        }
    }
}
