using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for UpdateAccommodation.xaml
    /// </summary>
    public partial class UpdateAccommodation : Window
    {
        public UpdateAccommodation(Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = new UpdateAccommodationViewModel(this, accommodation);
        }
    }
}
