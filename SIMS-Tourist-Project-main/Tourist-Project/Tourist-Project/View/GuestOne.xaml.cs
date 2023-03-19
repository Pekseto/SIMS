using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Observer;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for GuestOne.xaml
    /// </summary>
    public partial class GuestOne : Window, IObserver
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }

        public int SelectedIndex { get; set; }

        public GuestOne()
        {
            InitializeComponent();
            DataContext = this;

        }

        public void Update()
        {

        }

        private void SearchByNameClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }



        private void SearchByTypeClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        private void SearchByLocationClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        private void SearchByNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        private void SearchByDaysForResClick(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }


        /*private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxItem.ContentProperty.Equals("Name"))
            {
                var searchWindow = new SearchWindow();
                searchWindow.Show();
            }
            else if(ComboBoxItem.ContentProperty.Equals("Location"))
            {
                var searchWindow = new SearchWindow();
                searchWindow.Show();
            }
            else if(ComboBoxItem.ContentProperty.Equals("Type"))
            {
                var searchWindow = new SearchWindow();
                searchWindow.Show();
            }
            else if(ComboBoxItem.ContentProperty.Equals("Number of Guests"))
            {
                var searchWindow = new SearchWindow();
                searchWindow.Show();
            }
            else
            {
                var searchWindow = new SearchWindow();
                searchWindow.Show();
            }
        }*/



    }
}
