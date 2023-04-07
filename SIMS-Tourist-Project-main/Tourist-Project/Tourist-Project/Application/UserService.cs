using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repository;

namespace Tourist_Project.Application
{
    public class UserService
    {
        private readonly UserRepository userRepository = new();

        public UserService()
        {

        }

        public User GetOne(int id)
        {
            return userRepository.GetOne(id);
        }
    }
}
