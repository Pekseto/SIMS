using System;

namespace Tourist_Project.Model
{

    public class Accommodation
    {
        int Id { get; set; }
        string Name { get; set; }
        Location Location { get; set; }
        AccommodationType type { get; set; }
        int maxGuestNum { get; set; }
        int minStayingDays { get; set; }
        int daysBeforeCancel { get; set; }
        Image image { get; set; }
        int ownerId { get; set; }

        public Accommodation(int id, string name, Location location, AccommodationType type, int maxGuestNum, int minStayingDays, int daysBeforeCancel, Image image, int ownerId)
        {
            Id = id;
            Name = name;
            Location = location;
            this.type = type;
            this.maxGuestNum = maxGuestNum;
            this.minStayingDays = minStayingDays;
            this.daysBeforeCancel = daysBeforeCancel;
            this.image = image;
            this.ownerId = ownerId;
        }

        public string[] ToCSV()
        {
            //TO_DO enum
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), maxGuestNum.ToString(), minStayingDays.ToString(), daysBeforeCancel.ToString(), image.Id.ToString(), ownerId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            //TO_DO Type = Convert.ToInt32(values[3]);
            maxGuestNum = Convert.ToInt32(values[4]);
            minStayingDays= Convert.ToInt32(values[5]);
            daysBeforeCancel = Convert.ToInt32(values[6]);
            image = new Image() { Id = Convert.ToInt32(values[7]) };
            ownerId = Convert.ToInt32(values[8]);
        }
    }
}
