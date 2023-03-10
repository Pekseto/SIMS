using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Storage;

namespace Tourist_Project.Dao
{
    public class ImageDao
    {
        private ImageStorage imageStorage;
        private List<Image> images;

        public ImageDao()
        {
            imageStorage = new ImageStorage();
            images = new List<Image>();
        }
        public int NextId()
        {
            //Trebalo bi da vrati dobar Id proveri sa Id koji ce se dobiti za Tour
            return imageStorage.GetAll().Max(image => image.Id) + 1;
        }

        public void Add(Image image)
        {
            image.Id = NextId();
            images.Add(image);
            imageStorage.Save(images); //Hoce li proslediti listu sa samo jednim objektom? Zbog kreiranja nove u konstruktoru
        }

        public List<Image> GetAll()
        {
            //TODO: Ostavljeno da se odradi posle zbog povezivanja svih klasa

            return images;
        }

        public void Save(List<Image> images)
        {
            imageStorage.Save(images);
            this.images = images; //Potrebno?
        }
    }
}
