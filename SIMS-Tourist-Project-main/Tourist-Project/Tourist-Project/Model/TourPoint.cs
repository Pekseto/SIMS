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

        public TourPoint(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
        }
    }
}
