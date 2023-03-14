using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
    public class Tour : ISerializable
    {
        int id;
        public int Id
        {
            get => id;
            set => id = value;
        }

        int locationId;
        public int LocationId
        {
            get => locationId;
            set
            {
                if (value != locationId)
                {
                    locationId = value;
                    OnPropertyChanged("LocationId");
                }
            }
        }
        
        string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        string description;
        public string Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        string language;
        public string Language
        {
            get => language;
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        int maxGuestsNumber;
        public int MaxGuestsNumber
        {
            get => maxGuestsNumber;
            set
            {
                if (value != maxGuestsNumber)
                {
                    maxGuestsNumber = value;
                    OnPropertyChanged("MaxGuestsNumber");
                }
            }
        }

        List<TourPoint> tourPoints;
        public List<TourPoint> TourPoints
        {
            get => tourPoints;
            set => tourPoints = value;
        }

        DateTime startTime;
        public DateTime StartTime
        {
            get => startTime;
            set
            {
                if (value != startTime)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }

        int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        int imageId;
        public int ImageId
        {
            get => imageId;
            set
            {
                if (value != imageId)
                {
                    imageId = value;
                    OnPropertyChanged("ImageId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tour()
        {
            this.tourPoints = new List<TourPoint>();
        }

        public Tour(string name, int locationId, string description, string language, int maxGuestsNumber, DateTime startTime, int duration, int imageId)
        {
            this.TourPoints = new List<TourPoint>();

            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxGuestsNumber = maxGuestsNumber;
            this.StartTime = startTime;
            this.Duration = duration;
            this.imageId = imageId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                Language,
                MaxGuestsNumber.ToString(),
                StartTime.ToString(),
                Duration.ToString(),
                ImageId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Description = values[3];
            Language = values[4];
            MaxGuestsNumber = int.Parse(values[5]);
            StartTime = DateTime.Parse(values[6]);
            Duration = int.Parse(values[7]);
            ImageId = int.Parse(values[8]);
        }
    }
}
