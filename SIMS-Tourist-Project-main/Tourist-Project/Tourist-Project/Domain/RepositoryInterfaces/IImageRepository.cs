using System.Collections.Generic;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        public List<Image> GetAll();
        public Image Save(Image image);
        public int NextId();
        public void Delete(int id);
        public Image Update(Image image);
        public Image GetById(int id);
    }

}