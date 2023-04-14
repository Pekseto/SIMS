using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

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

        public User Update(User user)
        {
            return repository.Update(user);
        }
    }
}
