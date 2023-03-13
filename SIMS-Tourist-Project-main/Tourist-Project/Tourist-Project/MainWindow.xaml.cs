using System;
using System.Collections.Generic;
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
using Tourist_Project.View;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void OwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var ownerShowWindow = new OwnerShowWindow();
            ownerShowWindow.Show();
        }
        private void Guest1ButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void Guest2ButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void GuideButtonClick(object sender, RoutedEventArgs e)
        {
            var guideShowWindow = new GuideShowWindow();
            guideShowWindow.Show();
        }
    }
}
