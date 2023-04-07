using System.Collections.Generic;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IService<T>
    {
        public T Create(T instance);
        public List<T> GetAll();
        public T Get(int id);
        public T Update(T instance);
        public void Delete(int id);
    }
}
