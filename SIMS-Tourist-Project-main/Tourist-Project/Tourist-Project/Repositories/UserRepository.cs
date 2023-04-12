using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Data/users.csv";
        private readonly Serializer<User> serializer;
        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        public User? GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public User GetOne(int id)
        {
            return users.Find(user => user.Id == id);
        }
    }
}

