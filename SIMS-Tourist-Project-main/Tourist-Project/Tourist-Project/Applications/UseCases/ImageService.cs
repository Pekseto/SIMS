using System.Collections.Generic;
using System.Linq;
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

        public Image? GetByUrl(string url)
        {
            return imageRepository.GetByUrl(url);
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

        public List<int> CreateImages(string url)
        {
            var ids = new List<int>();
            if (imageRepository.GetByUrl(url) != null)
            {
                ids.Add(imageRepository.GetByUrl(url).Id);
            }
            else
            {
                Image newImage = new(url);
                var savedImage = Create(newImage);
                ids.Add(savedImage.Id);
            }
            return ids;
        }
        public string? FormIdesString(string url)
        {
            var ids = CreateImages(url);
            if (ids.Count <= 0) return null;
            var ides = ids.Aggregate(string.Empty, (current, imageId) => current + (imageId + ","));
            ides = ides.Remove(ides.Length - 1);
            return ides;
        }
    }

}