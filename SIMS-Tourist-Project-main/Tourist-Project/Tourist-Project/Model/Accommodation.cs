using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public enum AccommodationType { Apartment, House, Cottage }
	public class Accommodation : ISerializable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Location Location { get; set; }
		public AccommodationType type { get; set; }
		public int maxGuestNum { get; set; }
		public int minStayingDays { get; set; }
		public int daysBeforeCancel { get; set; }
		public Image image { get; set; }
		public int ownerId { get; set; }
        public User user { get; set; }
        public Accommodation() { }
        public Accommodation(string name, Location location, AccommodationType type, int maxGuestNum, int minStayingDays, int daysBeforeCancel, Image image)
        {
            Name = name;
            Location = location;
            this.type = type;
            this.maxGuestNum = maxGuestNum;
            this.minStayingDays = minStayingDays;
            this.daysBeforeCancel = daysBeforeCancel;
            this.image = image;
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
            minStayingDays = Convert.ToInt32(values[5]);
            daysBeforeCancel = Convert.ToInt32(values[6]);
            image = new Image() { Id = Convert.ToInt32(values[7]) };
            ownerId = Convert.ToInt32(values[8]);
        }
    }
}
