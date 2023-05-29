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
using Tourist_Project.View;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using System.Collections.ObjectModel;
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.View
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {

        
        public ReservationWindow(Accommodation SelectedAccommodation, User user)
        {
            InitializeComponent();
            this.DataContext = new CreateReservationViewModel(this, SelectedAccommodation, user);

            //AccommodationName = SelectedAccommodationDTO.Name;
            //Location = SelectedAccommodationDTO.LocationFullName;
            //Type = SelectedAccommodationDTO.AccommodationType.ToString();
            //AvailableDates = SelectedAccommodationDTO.AvailableDates;
            //AccommodationId = SelectedAccommodationDTO.AccommodationId;
           // AccommodationDTOs = AccommodationDtoRepository.

        }





    }
}