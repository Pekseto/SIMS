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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourist_Project.Model;
using Tourist_Project.View;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<GuestReview> guestReviews { get; set; }
        public static ObservableCollection<Reservation> reservations { get; set; }
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            guestReviews = new ObservableCollection<GuestReview>();
            reservations = new ObservableCollection<Reservation>();
        }

        private void OwnerButtonClick(object sender, RoutedEventArgs e)
        {
            if (guestReviews.Any())
            {
                foreach (var guestReview in guestReviews)
                {
                    foreach (var reservation in reservations)
                    {
                        int daysSinceCheckOut = (int)(reservation.CheckOut - DateTime.Now).TotalDays;
                        if (guestReview.guestId == reservation.guestId) 
                        {
                            if (!guestReview.IsReviewed() && daysSinceCheckOut < 5)
                            {
                                MessageBoxResult result = MessageBox.Show("You have unreviewd guests. Do you want to grade them?", "Grade guest",
                                MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (result == MessageBoxResult.Yes)
                                {
                                    var guestRevisionWindow = new GuestRevision(guestReview.guestId, guestReview.ownerId);
                                    guestRevisionWindow.Show();
                                }
                                else
                                {
                                    var ownerShowWindow = new OwnerShowWindow();
                                    ownerShowWindow.Show();
                                }
                            } 
                        }
                    }
                }
            }
            else
            {
                var ownerShowWindow = new OwnerShowWindow();
                ownerShowWindow.Show();
            }
        }
        private void Guest1ButtonClick(object sender, RoutedEventArgs e)
        {
            var guestOne = new GuestOne();
            guestOne.Show();
        }
        private void Guest2ButtonClick(object sender, RoutedEventArgs e)
        {
            var guestTwoShowWindow = new GuestTwoWindow();
            guestTwoShowWindow.Show();
        }
        private void GuideButtonClick(object sender, RoutedEventArgs e)
        {
            var guideShowWindow = new GuideShowWindow();
            guideShowWindow.Show();
        }
    }
}
