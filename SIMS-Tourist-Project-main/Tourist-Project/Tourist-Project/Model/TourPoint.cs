using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class TourPoint : ISerializable
    {
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        string name;
        public string Name
        {
            get => name;
            set => name = value;
        }
        private int tourId;
        public int TourId
        {
            get => tourId;
            set => tourId = value;
        }

        private bool visited;
        public bool Visited
        {
            get => visited;
            set => visited = value;
        }

        public TourPoint()
        {
            this.visited = false;
        }

        public TourPoint(string name, int tourId)
        {
            this.name = name;
            this.tourId = tourId;
            this.visited = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                tourId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            name = values[1];
            tourId = int.Parse(values[2]);
        }
    }
}
