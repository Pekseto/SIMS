using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public enum AccommodationType { Apartment, House, Cottage }
	public class Accommodation : ISerializable
    {
		public int Id { get; set; }
		string Name { get; set; }
		Location Location { get; set; }
		AccommodationType type { get; set; }
		int maxGuestNum { get; set; }
		int minStayingDays { get; set; }
		int daysBeforeCancel { get; set; }
		Image image { get; set; }
		int ownerId { get; set; }
        public User user { get; set; }

        public Accommodation() { }
        public Accommodation(string name, int locationId, AccommodationType type, int maxGuestNum, int minStayingDays, int daysBeforeCancel, int imageId)
        {
            Name = name;
            Location = new Location();
            this.type = type;
            this.maxGuestNum = maxGuestNum;
            this.minStayingDays = minStayingDays;
            this.daysBeforeCancel = daysBeforeCancel;
            this.image = new Image();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), type.ToString(), maxGuestNum.ToString(), minStayingDays.ToString(), daysBeforeCancel.ToString(), image.Id.ToString(), ownerId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            type = Enum.Parse<AccommodationType>(values[3]);
            maxGuestNum = Convert.ToInt32(values[4]);
            minStayingDays= Convert.ToInt32(values[5]);
            daysBeforeCancel = Convert.ToInt32(values[6]);
            image = new Image() { Id = Convert.ToInt32(values[7]) };
            ownerId = Convert.ToInt32(values[8]);
        }
    }
}
