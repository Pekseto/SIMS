using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist_Project.Model
{
    public class TourPoint
    {
        int id { get; set; }
        string name { get; set; }
        int tourId { get; set; }

        public TourPoint(int id, string name, int tourId)
        {
            this.id = id;
            this.name = name;
            this.tourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                tourId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            tourId = Convert.ToInt32(values[2]);
        }
    }
}
