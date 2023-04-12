using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for GuestTwoWindow.xaml
    /// </summary>
    public partial class GuestTwoWindow : Window
    {
        
        public GuestTwoWindow(User user)
        {
            InitializeComponent();
            DataContext = new GuestTwoWindowViewModel(user, toursDataGrid);
            
        }

        
    }
}
