﻿using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for RateGuestWindow.xaml
    /// </summary>
    public partial class RateGuestWindow : Window
    {
        public RateGuestWindow(Notification notification)
        {
            InitializeComponent();
            DataContext = new RateGuestViewModel(notification, this);
        }
    }
}
