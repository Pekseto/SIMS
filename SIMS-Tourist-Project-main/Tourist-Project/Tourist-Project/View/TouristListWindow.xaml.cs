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
        private UserRepository userRepository;
        public TourAttendance SelectedTourAttendance { get; set; }
        private TourPoint selectedTourPoint;
        private TourPointRepository tourPointRepository;
        public TouristListWindow(TourPoint selectedTourPoint)
        {
            InitializeComponent();
            DataContext = this;
            this.selectedTourPoint = selectedTourPoint;
            tourAttendanceRepository = new TourAttendanceRepository();
            userRepository = new UserRepository();
            tourPointRepository = new TourPointRepository();
            TourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceRepository.GetAll().FindAll(attendace => attendace.TourId == selectedTourPoint.TourId));
            foreach(TourAttendance attendace in TourAttendances)
            {
                attendace.User = userRepository.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointRepository.GetAll().Find(point => point.Id == attendace.CheckPointId);

            }

        }

        public void CallOutClick(object sender, RoutedEventArgs e)
        {
            SelectedTourAttendance.TourPoint = selectedTourPoint;
            SelectedTourAttendance.CheckPointId = selectedTourPoint.Id;
            tourAttendanceRepository.Update(SelectedTourAttendance);
            TourAttendances.Clear();
            foreach (TourAttendance attendance in tourAttendanceRepository.GetAll().FindAll(attendace => attendace.TourId == selectedTourPoint.TourId))
            {
                TourAttendances.Add(attendance);
            }
            foreach (TourAttendance attendace in TourAttendances)
            {
                attendace.User = userRepository.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointRepository.GetAll().Find(point => point.Id == attendace.CheckPointId);
            }
        }
    }
}
