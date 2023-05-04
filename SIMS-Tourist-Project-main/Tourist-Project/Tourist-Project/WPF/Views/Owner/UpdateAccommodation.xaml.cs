using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
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
