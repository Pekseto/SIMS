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
using System.Windows.Shapes;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuideShowWindow.xaml
    /// </summary>
    public partial class GuideShowWindow : Window
    {
        public GuideShowWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var createTourWindow = new CreateTour();
            createTourWindow.Show();
        }
    }
}
