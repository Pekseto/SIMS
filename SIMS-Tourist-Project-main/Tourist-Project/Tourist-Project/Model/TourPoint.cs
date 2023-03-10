using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist_Project.Model
{
    public class TourPoint
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
        int tourId;
        public int TourId
        {
            get => tourId;
            set => tourId = value;
        }

        public TourPoint(int id, string name, int tourId)
        {
            this.Id = id;
            this.Name = name;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                TourId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            TourId = int.Parse(values[2]);
        }
    }
}
