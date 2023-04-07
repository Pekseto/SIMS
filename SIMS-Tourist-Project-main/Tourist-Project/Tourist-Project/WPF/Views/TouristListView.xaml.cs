using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repository;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for TouristListWindow.xaml
    /// </summary>
    public partial class TouristListView : Window
    {
        public TouristListView(TourPoint selectedTourPoint)
        {
            InitializeComponent();
            DataContext = new TouristListViewModel(selectedTourPoint);
            
        }
    }
}
