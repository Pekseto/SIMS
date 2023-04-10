using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for TourLiveWindow.xaml
    /// </summary>
    public partial class TourLiveView : Window
    {

        public TourLiveView(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = new TourLiveViewModel(selectedTour);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var service = new TourPointService();
            service.RequestClose += (s, ev) => this.Close();
        }
    }
}
