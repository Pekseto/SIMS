using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Dao;
using Tourist_Project.Model;

namespace Tourist_Project.Controller
{
    public class ImageController
    {
        private ImageDao imageDao;

        public ImageController()
        {
            imageDao = new ImageDao();
        }

        public List<Image> GetAll()
        {
            return imageDao.GetAll();
        }

        public void Save(List<Image> images)
        {
            imageDao.Save(images);
        }

        public void Create(Image image)
        {
            imageDao.Add(image);
        }

        public Image GetOne(int id)
        {
            return imageDao.GetAll().Find(image => image.Id == id);
        }

        public int GetId(string url)
        {
            return imageDao.GetAll().Find(image => image.Url == url).Id;
        }
    }

}
