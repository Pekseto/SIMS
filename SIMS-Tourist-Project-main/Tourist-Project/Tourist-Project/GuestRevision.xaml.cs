using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Model;
using Tourist_Project.Repository;

namespace Tourist_Project
{
    /// <summary>
    /// Interaction logic for GuestRevision.xaml
    /// </summary>
    public partial class GuestRevision : Window
    {
        public GuestReview ReviewingGuest { get; set; } 
        private readonly GuestReviewRepository guestReviewRepository;
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

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            ReviewingGuest.cleanlinessGrade = int.Parse(Cleanliness.Text);
            ReviewingGuest.ruleGrade = int.Parse(Rules.Text);
            ReviewingGuest.comment = Comment.Text;
            GuestReview savedGuestReview = guestReviewRepository.Update(ReviewingGuest);
            Close();
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
