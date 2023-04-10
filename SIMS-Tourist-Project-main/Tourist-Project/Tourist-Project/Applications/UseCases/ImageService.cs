using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class ImageService : IService<Image>
    {
        private static readonly Injector injector = new();

        private readonly IImageRepository imageRepository =
            injector.CreateInstance<IImageRepository>();
        public ImageService()
        {
        }

        public Image Create(Image image)
        {
            return imageRepository.Save(image);
        }

        public List<Image> GetAll()
        {
            return imageRepository.GetAll();
        }

        public Image Get(int id)
        {
            return imageRepository.GetById(id);
        }

        public Image Update(Image image)
        {
            return imageRepository.Update(image);
        }

        public void Delete(int id)
        {
            imageRepository.Delete(id);
        }

        public void Save(Image image)
        {
            imageRepository.Save(image);
        }
    }

}