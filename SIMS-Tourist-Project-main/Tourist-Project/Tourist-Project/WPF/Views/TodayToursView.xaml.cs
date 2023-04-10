using System.Collections.ObjectModel;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for GuideShowWindow.xaml
    /// </summary>
    public partial class TodayToursView : Window
    {
        public TodayToursView()
        {
            InitializeComponent();
            DataContext = new TodayToursViewModel(this);
        }
    }
}
