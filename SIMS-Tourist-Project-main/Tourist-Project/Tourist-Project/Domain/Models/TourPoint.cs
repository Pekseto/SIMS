using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class TourPoint : ISerializable
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
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }

        }
        private int tourId;
        public int TourId
        {
            get => tourId;
            set
            {
                if (tourId != value)
                {
                    tourId = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool visited;
        public bool Visited
        {
            get => visited;
            set
            {
                if (visited != value)
                {
                    visited = value;
                    OnPropertyChanged("Visited");
                }
            }
        }

        public TourPoint()
        {
            visited = false;
        }

        public TourPoint(string name, int tourId)
        {
            this.name = name;
            this.tourId = tourId;
            visited = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                tourId.ToString(),
                visited.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            name = values[1];
            tourId = int.Parse(values[2]);
            visited = bool.Parse(values[3]);
        }
    }
}
