using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public enum AccommodationType { Apartment, House, Cottage }
	public class Accommodation : ISerializable
	{
        int id;
		public int Id 
        {
            get => id; 
            set => id = value;
        }
        string name;
		public string Name
        {
            get => name;    
            set
            {
                if(value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        Location location;
        public Location Location
        {
            get => location;
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }
        AccommodationType type;
        public AccommodationType Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }
        int maxGuestNum;
        public int MaxGuestNum
        {
            get => maxGuestNum;
            set
            {
                if (value != maxGuestNum)
                {
                    maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }
        int minStayingDays;
        public int MinStayingDays
        {
            get => minStayingDays;
            set
            {
                if (value != minStayingDays)
                {
                    minStayingDays = value;
                    OnPropertyChanged();
                }
            }
        }
        int daysBeforeCancel;
        public int DaysBeforeCancel
        {
            get => daysBeforeCancel;
            set
            {
                if (value != daysBeforeCancel)
                {
                    daysBeforeCancel = value;
                    OnPropertyChanged();
                }
            }
        }
        Image image;
        public Image Image
        {
            get => image;
            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged();
                }
            }
        }
        //public int ownerId { get; set; }
        public User user { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Accommodation() { }
        public Accommodation(string name, Location location, AccommodationType type, int maxGuestNum, int minStayingDays, int daysBeforeCancel, Image image)
        {
            Name = name;
            Location = location;
            this.Type = type;
            this.MaxGuestNum = maxGuestNum;
            this.MinStayingDays = minStayingDays;
            this.DaysBeforeCancel = daysBeforeCancel;
            this.Image = image;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), Type.ToString(), MaxGuestNum.ToString(), MinStayingDays.ToString(), DaysBeforeCancel.ToString(), Image.Id.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Type = Enum.Parse<AccommodationType>(values[3]);
            MaxGuestNum = Convert.ToInt32(values[4]);
            MinStayingDays = Convert.ToInt32(values[5]);
            DaysBeforeCancel = Convert.ToInt32(values[6]);
            Image = new Image() { Id = Convert.ToInt32(values[7]) };
            //user = new User() { Id = Convert.ToInt32(values[8]) };
        }
    }
}
