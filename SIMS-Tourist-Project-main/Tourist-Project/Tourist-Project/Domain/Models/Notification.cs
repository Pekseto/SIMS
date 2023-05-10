using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Notification : ISerializable, INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                if (value == id) return;
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string type;
        public string Type
        {
            get => type;
            set
            {
                if(value == type) return;
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private bool notified;
        public bool Notified
        {
            get => notified;
            set
            {
                if(value == notified) return;
                notified = value;
                OnPropertyChanged("Notified");
            }
        }

        private int typeId;
        public int TypeId
        {
            get => typeId;
            set
            {
                if(value == typeId) return;
                typeId = value;
                OnPropertyChanged("TypeId");
            }
        }
        public Notification(){}
        public Notification(string type, bool notified, int typeId)
        {
            Type = type;
            Notified = notified;
            TypeId = typeId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Type,
                Notified.ToString(),
                TypeId.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Type = values[1];
            Notified = bool.Parse(values[2]);
            TypeId = int.Parse(values[3]);
        }
        public override string ToString()
        {
            return Type switch
            {
                "GuestRate" => "You have unrated guest.",
                "Forum" => "A new forum has opened. Check it out",
                "Recommended" => "You have a new \nrecommendation.",
                "Reviews" => "NEW!\nGuest has rated your \naccommodation.",
                _ => string.Empty
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
