using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public User GetOne(int id);
        public User? GetByUsername(string username);
        public User Update(User user);

    }
}
