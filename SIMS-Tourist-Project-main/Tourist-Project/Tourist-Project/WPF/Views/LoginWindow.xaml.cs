using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.Repository;

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
            User user = repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    var mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    Close();
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
