using System.Windows;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for GeneratePDF.xaml
    /// </summary>
    public partial class GeneratePDF : Window
    {
        public GeneratePDF(AccommodationViewModel accommodationViewModel)
        {
            InitializeComponent();
            DataContext = new GeneratePdfViewModel(accommodationViewModel);
        }
    }
}
