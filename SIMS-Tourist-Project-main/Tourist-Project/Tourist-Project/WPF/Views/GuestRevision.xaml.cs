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
        public GuestReview ReviewingGuest { get; set; }
        private readonly GuestReviewRepository guestReviewRepository = new();
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public GuestRevision(int ownerId, int guestId, GuestReview reviewingGuest)
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
            _ = guestReviewRepository.Update(ReviewingGuest);
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
