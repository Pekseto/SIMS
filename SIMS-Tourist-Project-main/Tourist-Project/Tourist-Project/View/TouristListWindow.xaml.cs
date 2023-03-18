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
    /// Interaction logic for TouristListWindow.xaml
    /// </summary>
    public partial class TouristListWindow : Window
    {
        public ObservableCollection<TourAttendance> TourAttendances { get; set; }
        private TourAttendanceRepository tourAttendanceRepository;
        public TouristListWindow(TourPoint selectedTourPoint)
        {
            InitializeComponent();
            DataContext = this;
            tourAttendanceRepository = new TourAttendanceRepository();
            TourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceRepository.GetAll().FindAll(attendace => attendace.TourId == selectedTourPoint.TourId));
        }
    }
}
