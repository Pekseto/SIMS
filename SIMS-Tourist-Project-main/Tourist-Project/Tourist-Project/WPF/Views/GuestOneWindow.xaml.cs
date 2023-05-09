using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Repository;
using Tourist_Project.DTO;
using System.Collections.Generic;
using System;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Image = Tourist_Project.Domain.Models.Image;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for GuestOne.xaml
    /// </summary>
    /// 
    public enum AccommodationType { House, Cottage, Apartment }
    public partial class GuestOneWindow : Window
    {
        public GuestOneWindow(User user)
     
        {
            InitializeComponent();
            this.DataContext = new GuestOneViewModel(this, GuestOneDataGrid);    
        }
    }
}