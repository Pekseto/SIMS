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
using Tourist_Project.Model;
using System.Collections.ObjectModel;
using Tourist_Project.Observer;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window, IObserver
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }

        public SearchWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }


        public void Update()
        {
        }

        

    }
    
}
