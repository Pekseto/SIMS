using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views.GuestOne;
using Tourist_Project.Domain.Models;
using System.Windows;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestForumViewModel 
    {
        private Window _window;
        public String SelectedCountry { get; set; }
        public String SelectedCity { get; set; }

        public String ForumComment = "";

        private LocationService _locationService = new();
        private ForumService _forumService = new();

        public User User { get; set; } = new();

        public ObservableCollection<String> Cities { get; set; }
        public ObservableCollection<String> Countries { get; set; }

        public ICommand CreateForum_Command { get; set; }
        public ICommand ReadForum_Command { get; set; }

        public ICommand Home_Command { get; set; }
        public GuestForumViewModel(User user,Window window)
        {
            _window = window;
            Cities = new ObservableCollection<String>(_locationService.GetAllCities());
            Countries = new ObservableCollection<String>(_locationService.GetAllCountries());
            CreateForum_Command = new RelayCommand(CreateForum, CanCreateForum);
            ReadForum_Command = new RelayCommand(ReadForum, CanRead);
            Home_Command = new RelayCommand(Home, CanHome);
            User = user;

        }

        private bool CanHome()
        {
            return true;
        }

        private void Home()
        {
            _window.Close();
        }
        private bool CanRead()
        {
            return true;
        }

        private void ReadForum()
        {
            int id = _locationService.GetId(SelectedCity, SelectedCountry);
            Forum forum = new Forum();
            forum.LocationId = id;
            forum.IsClosed = false;
            forum.UserId = User.Id;
            forum.CommentsIdsCsv = ForumComment;
            var forumsToRead = new LocationForumView(SelectedCity, SelectedCountry,forum);
            forumsToRead.Show();
        }
        private bool CanCreateForum()
        {
            return true;
        }

        private void CreateForum()
        {
            int id = _locationService.GetId(SelectedCity, SelectedCountry);
            Forum forum = new Forum();
            forum.LocationId = id;
            forum.IsClosed = false;
            forum.UserId = User.Id;
            forum.CommentsIdsCsv = ForumComment;
            _forumService.Create(forum);
            MessageBox.Show("Forum created");
            var locationForum = new LocationForumView(SelectedCity, SelectedCountry, forum);
            locationForum.Show();
        }

    }
}
