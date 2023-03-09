using System;

namespace Tourist_Project.Model
{
    public enum AccommodationType { Apartment, House, Cottage }
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

    }
}
