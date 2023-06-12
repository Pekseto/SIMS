using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Applications.UseCases;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuestForumsViewModel
    {
        private ForumService _forumService =  new ForumService();

        public ObservableCollection<ForumViewModel> Forums = new ObservableCollection<ForumViewModel>();  

        public GuestForumsViewModel()
        {
            Forums = new ObservableCollection<ForumViewModel>(_forumService.GetAll().Select(forum => new ForumViewModel(forum.Id)));
        }

        
    }
}
