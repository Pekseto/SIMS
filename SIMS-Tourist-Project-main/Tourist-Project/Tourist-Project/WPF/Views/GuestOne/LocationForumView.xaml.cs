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
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.Views.GuestOne
{
    /// <summary>
    /// Interaction logic for LocationForumView.xaml
    /// </summary>
    public partial class LocationForumView : Window
    {
        public LocationForumView(String selectedCity, String selectedCountry, Forum forum)
        {
            InitializeComponent();
            this.DataContext = new LocationForumViewModel(selectedCity, selectedCountry, this, forum);
        }
    }
}
