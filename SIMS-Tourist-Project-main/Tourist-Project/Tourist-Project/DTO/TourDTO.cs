using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.DTO
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

        private int spotsLeft;
        public int SpotsLeft
        {
            get { return spotsLeft; }
            set
            {
                if (value != spotsLeft)
                {
                    spotsLeft = value;
                    OnPropertyChanged(nameof(SpotsLeft));
                }
            }
        }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public Status Status { get; set; }

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
            Status = tour.Status;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
