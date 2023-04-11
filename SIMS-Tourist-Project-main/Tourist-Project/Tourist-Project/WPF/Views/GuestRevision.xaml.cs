using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for GuestRevision.xaml
    /// </summary>
    public partial class GuestRevision : Window
    {
        public GuestRating ReviewingGuest { get; set; }
        private readonly GuestRateRepository guestRateRepository = new();
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public GuestRevision(int ownerId, int guestId, GuestRating reviewingGuest)
        {
            InitializeComponent();
            DataContext = this;
            OwnerId = ownerId;
            GuestId = guestId;
            ReviewingGuest = reviewingGuest;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            ReviewingGuest.CleanlinessGrade = int.Parse(Cleanliness.Text);
            ReviewingGuest.RuleGrade = int.Parse(Rules.Text);
            ReviewingGuest.Comment = Comment.Text;
            _ = guestRateRepository.Update(ReviewingGuest);
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
