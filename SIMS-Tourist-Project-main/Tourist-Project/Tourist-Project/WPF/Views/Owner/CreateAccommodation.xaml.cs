using System.Windows;
using Tourist_Project.WPF.ViewModels;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for CreateAccommodation.xaml
    /// </summary>
    public partial class CreateAccommodation : Window
    {
        public CreateAccommodation()
        {
            InitializeComponent();
            DataContext = new CreateAccommodationViewModel(this);
        }
    }
}
