using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.View;
using Tourist_Project.WPF.Views.Owner;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User LoggedInUser { get; set; }
        public MainWindow(User user)
        {
            DataContext = this;
            InitializeComponent();
            LoggedInUser = user;
        }

        private void OwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var ownerShowWindow = new OwnerMainWindow(LoggedInUser);
            ownerShowWindow.Show();
        }
        private void Guest1ButtonClick(object sender, RoutedEventArgs e)
        {
            var guestOne = new GuestOne(LoggedInUser);
            guestOne.Show();
        }
        private void Guest2ButtonClick(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser.Role == UserRole.guest2)
            {
                var guestTwoWindow = new GuestTwoWindow(LoggedInUser);
                guestTwoWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong user role");
            }
        }
        private void GuideButtonClick(object sender, RoutedEventArgs e)
        {
            var guideShowWindow = new TodayToursView();
            guideShowWindow.Show();
        }
    }
}
