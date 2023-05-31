using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tourist_Project.Applications.UseCases;

namespace Tourist_Project.WPF.ViewModels.Owner
{
    public class ShowForumsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ForumViewModel> forums;
        public ObservableCollection<ForumViewModel> Forums
        {
            get => forums;
            set
            {
                if (value == forums) return;
                forums = value;
                OnPropertyChanged();
            }
        }

        private readonly NotificationService notificationService = new();

        public ShowForumsViewModel()
        {
            Forums = new ObservableCollection<ForumViewModel>(notificationService.GetAllByType("Forum").Select(notification => new ForumViewModel(notification.TypeId)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}