using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }

        public SearchWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

    }

}
