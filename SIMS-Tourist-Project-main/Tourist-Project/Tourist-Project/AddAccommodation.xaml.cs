using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tourist_Project.Controller;
using Tourist_Project.Model;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for AddAccommodation.xaml
    /// </summary>
    public partial class AddAccommodation : Window
    {
        public static AccommodationController accommodationController;
        public AddAccommodation(AccommodationController controller)
        {
            InitializeComponent();
            DataContext = this;
            accommodationController = controller;
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            Location location = new Location();
            Model.Image image = new Model.Image();
            Accommodation newAccommodation = new Accommodation(Name.Text, location.Id, Enum.Parse<AccommodationType>(Type.Text), int.Parse(MaxNumGuests.Text), int.Parse(MinStayingDays.Text), int.Parse(DaysBeforeCancel.Text), image.Id);
            accommodationController.Add(newAccommodation);
            Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
