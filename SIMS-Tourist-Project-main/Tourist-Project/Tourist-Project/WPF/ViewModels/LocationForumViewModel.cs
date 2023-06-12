using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;
using System.Collections.ObjectModel;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.ViewModels
{
    public class LocationForumViewModel
    {

        private Window _window; 
        public String SelectedCity { get; set; }
        public String SelectedCountry { get; set; }

        public String Comment { get; set; }

        public ICommand Home_Command { get; set; }

        public ICommand AddComment_Command { get; set; }

        private CommentService _commentService =  new CommentService();

        private ForumService _forumService = new ForumService();   
        
        public Forum Forum { get; set; } = new Forum();
        Comment NewComment { get; set; } = new Comment();

        public ObservableCollection<CommentViewModel> CommentsForForum { get; set; } = new();

        public LocationForumViewModel(String selectedCity, String selectedCountry, Window window, Forum forum)
        {
            _window = window;  
            Forum = forum;
            SelectedCity = selectedCity;
            SelectedCountry = selectedCountry;
            Home_Command = new RelayCommand(Home, CanHome);
            AddComment_Command = new RelayCommand(AddComment, CanAddComment);
            CommentsForForum = new ObservableCollection<CommentViewModel>(_commentService.GetAll().Where(comment => Forum.CommentsIds.Contains(comment.Id)).Select(comment => new CommentViewModel(comment, false, Forum)));
        }

        private bool CanHome()
        {
            return true;
        }

        private bool CanAddComment()
        {
            return true;
        }

        public void AddComment()
        {
            NewComment.Author = Author.Guest;
            NewComment.AuthorId = App.LoggedInUser.Id;
            NewComment.CommentText = Comment;
            NewComment.ReportNo = 0;
            _commentService.Create(NewComment);
            Forum.CommentsIds.Add(_commentService.Create(NewComment).Id);
            MessageBox.Show("Comment added to forum");
            _commentService.Create(NewComment);
            _window.Close();
        }

        private void Home()
        {
            _window.Close();
        }

        
    }
}
