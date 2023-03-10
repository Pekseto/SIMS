using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Storage
{
    public class ImageStorage
    {
        private const string fileName = "";
        private Serializer<Image> serializer;

        public ImageStorage()
        {
            serializer = new Serializer<Image>();
        }

        public List<Image> GetAll()
        {
            return serializer.FromCSV(fileName);
        }

        public void Save(List<Image> images)
        {
            serializer.ToCSV(fileName, images);
        }
    }
}
