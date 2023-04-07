using System.Collections.Generic;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T Save(T instance);
        public int NextId();
        public void Delete(int id);
        public T Update(T instance);
        public T GetById(int id);
    }

}