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
using Tourist_Project.Controller;
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

        public static AccommodationController accommodationController;

        public SearchWindow(AccommodationController accommodationController)
        {
            InitializeComponent();

            this.DataContext = this;

            accommodationController = new AccommodationController();
            accommodationController.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());



        }


        public void Update()
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodationController.GetAll())
                accommodationController.Add(accommodation);
        }

        

    }
    
}
