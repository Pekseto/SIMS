using System.Windows;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public OwnerMainWindow()
        {
            InitializeComponent();
            DataContext = new OwnerMainWindowViewModel(this);
        }
    }
}
