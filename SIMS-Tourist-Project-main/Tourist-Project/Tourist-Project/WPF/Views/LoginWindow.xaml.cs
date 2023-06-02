using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;
using Tourist_Project.View;
using Tourist_Project.WPF.Views.Guide;
using Tourist_Project.WPF.Views.GuestTwo;
using Tourist_Project.WPF.Views.Owner;
using Tourist_Project.WPF.Views.GuestOne;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserRepository repository;
        public string Username { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
            repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            var user = repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case UserRole.owner:
                        {
                            var ownerShowWindow = new OwnerMainWindow(user);
                            ownerShowWindow.Show();
                            Close();
                            break;
                        }
                        case UserRole.guest1:
                        {
                            var guestOne = new GuestOneWindow(user);
                            guestOne.Show();
                            Close();
                            break;
                        }
                        case UserRole.guest2:
                        {
                            var guestTwoWindow = new GuestTwoView(user);
                            guestTwoWindow.Show();
                            Close();
                            break;
                        }
                        case UserRole.guide:
                        {
                            var guideShowWindow = new TodayToursView(user);
                            guideShowWindow.Show();
                            Close();
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }
    }

}
