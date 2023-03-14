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
        public GuestReview guestReview { get; set; } 
        private readonly GuestReviewRepository guestReviewRepository;
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public GuestRevision(int ownerId, int guestId)
        {
            InitializeComponent();
            DataContext = this;
            guestReviewRepository = new GuestReviewRepository();
            OwnerId = ownerId;
            GuestId = guestId;
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            GuestReview newGuestReview = new GuestReview(OwnerId, GuestId, int.Parse(Cleanliness.Text), int.Parse(Rules.Text), Comment.Text);
            GuestReview savedGuestReview = guestReviewRepository.Save(newGuestReview);
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
