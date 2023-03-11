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
        private const string FilePath = "../../../Data/locations.csv";
        private Serializer<Image> serializer;

        public ImageStorage()
        {
            serializer = new Serializer<Image>();
        }

        public List<Image> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Image> images)
        {
            serializer.ToCSV(FilePath, images);
        }
    }
}
