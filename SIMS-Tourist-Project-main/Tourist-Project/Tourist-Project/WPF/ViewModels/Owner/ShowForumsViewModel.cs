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
        public CommentViewModel SelectedComment { get; set; }

        private readonly ForumService forumService = new();

        public ShowForumsViewModel()
        {
            Forums = new ObservableCollection<ForumViewModel>(forumService.GetAll().Select(forum => new ForumViewModel(forum.Id)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}