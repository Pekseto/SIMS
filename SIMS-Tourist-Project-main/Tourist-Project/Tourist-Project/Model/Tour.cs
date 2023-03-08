using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace Tourist_Project.Model
{
    public class Tour
    {
        int id { get; set; }
        string name { get; set; }
        int locationId { get; set; }
        Location location { get; set; }
        string description { get; set; }
        string language { get; set; }
        int maxGuestsNumber { get; set; }
        List<TourPoint> tourPoints { get; set; }
        DateTime tourStart { get; set; }
        int duration    { get; set; }
        int imageId { get; set; }
        Image image { get; set; }


        public Tour(int id, string name, int locationId, Location location, string description, string language, int maxGuestsNumber, List<TourPoint> tourPoints, DateTime tourStart, int duration, Image image)
        {
            this.id = id;
            this.name = name;
            this.locationId = locationId;
            this.location = location;
            this.description = description;
            this.language = language;
            this.maxGuestsNumber = maxGuestsNumber;
            this.tourPoints = new List<TourPoint>();
            this.tourPoints = tourPoints;
            this.tourStart = tourStart;
            this.duration = duration;
            this.image = image;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                locationId.ToString(),
                description,
                language,
                maxGuestsNumber.ToString(),
                tourStart.ToString(),
                duration.ToString(),
                imageId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            locationId = Convert.ToInt32(values[2]);
            description = values[3];
            language = values[4];
            maxGuestsNumber = Convert.ToInt32(values[5]);
            tourStart = Convert.ToDateTime(values[6]);
            duration = Convert.ToInt32(values[7]);
            imageId = Convert.ToInt32(values[8]);
        }
    }
}
