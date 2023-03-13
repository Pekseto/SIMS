using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Observer;
using Tourist_Project.Storage;

namespace Tourist_Project.Dao
{
    public class ImageDao
    {
        private List<IObserver> _observers;
        private ImageStorage storage;
        private List<Image> images;

        public ImageDao()
        {
            _observers = new List<IObserver>();
            storage = new ImageStorage();
            images = new List<Image>();
        }
        public int NextId()
        {
            //Trebalo bi da vrati dobar Id proveri sa Id koji ce se dobiti za Tour
            return storage.GetAll().Max(image => image.Id) + 1;
        }

        public void Add(Image image)
        {
            image.Id = NextId();
            images.Add(image);
            storage.Save(images); //Hoce li proslediti listu sa samo jednim objektom? Zbog kreiranja nove u konstruktoru
            NotifyObservers();
        }
        public void Delete(Image image)
        {
            images.Remove(image);
            storage.Save(images);
            NotifyObservers();
        }
        public void Update(Image image)
        {
            Image current = images.Find(c => c.Id == image.Id);
            int index = images.IndexOf(image);
            images.Remove(current);
            images.Insert(index, image);
            storage.Save(images);
            NotifyObservers();
        }
        public List<Image> GetAll()
        {
            //TODO: Ostavljeno da se odradi posle zbog povezivanja svih klasa

            return images;
        }
        public List<Image> GetById(int id)
        {
            return images.FindAll(c => c.Id == id);
        }

        public void Save(List<Image> images)
        {
            storage.Save(images);
            this.images = images; //Potrebno?
            NotifyObservers();
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
