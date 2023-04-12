using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repository;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Tourist_Project.Applications.UseCases
{
    public class UserService
    {
        private static readonly Injector injector = new();

        private readonly IUserRepository repository = injector.CreateInstance<IUserRepository>();

        public UserService()
        {

        }

        public User GetOne(int id)
        {
            return repository.GetOne(id);
        }

        
    }
}
