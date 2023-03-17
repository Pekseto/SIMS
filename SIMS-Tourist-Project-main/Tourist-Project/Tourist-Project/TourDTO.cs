using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Model;

namespace Tourist_Project
{
    public class TourDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuestsNumber { get; set; }
        private int guestsLeft;
        public int GuestsLeft
        {
            get { return guestsLeft; }
            set
            {
                if (value != guestsLeft)
                {
                    guestsLeft = value;
                    OnPropertyChanged(nameof(GuestsLeft));
                }
            }
        }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public TourDTO()
        {
            Location = new Location();
        }

        public TourDTO(Tour tour)
        {
            Id = tour.Id;
            LocationId = tour.LocationId;
            Location = new Location();
            Name = tour.Name;
            Description = tour.Description;
            Language = tour.Language;
            MaxGuestsNumber = tour.MaxGuestsNumber;
            StartTime = tour.StartTime;
            Duration = tour.Duration;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
