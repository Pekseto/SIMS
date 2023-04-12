using System.Windows;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
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
