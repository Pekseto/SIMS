using System.Windows;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project.View
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
            guestReviewRepository = new GuestReviewRepository();
            OwnerId = ownerId;
            GuestId = guestId;
            ReviewingGuest = reviewingGuest;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            ReviewingGuest.cleanlinessGrade = int.Parse(cleanliness.Text);
            ReviewingGuest.ruleGrade = int.Parse(rules.Text);
            ReviewingGuest.comment = comment.Text;
            _ = guestReviewRepository.Update(ReviewingGuest);
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
