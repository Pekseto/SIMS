using System.Windows;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for OwnerReviewsView.xaml
    /// </summary>
    public partial class OwnerReviewsView : Window
    {
        public OwnerReviewsView()
        {
            InitializeComponent();
            DataContext = new OwnerReviewsViewModel();
        }
    }
}
