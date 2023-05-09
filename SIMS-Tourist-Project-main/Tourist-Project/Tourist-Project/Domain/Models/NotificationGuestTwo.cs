using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum NotificationType
    {
        ConfirmPresence, TourAccepted, NewTour
    }
    public class NotificationGuestTwo : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Opened { get; set; }
        public NotificationType NotificationType { get; set; }

        public NotificationGuestTwo()
        {
            
        }

        public NotificationGuestTwo(int userId, int tourId, DateTime date, NotificationType notificationType)
        {
            UserId = userId;
            TourId = tourId;
            Date = date;
            Opened = false;
            NotificationType = notificationType;

            switch (notificationType)
            {
                case NotificationType.ConfirmPresence:
                    Title = "Confirm your presence";
                    break;
                case NotificationType.TourAccepted:
                    Title = "Tour accepted";
                    break;
                case NotificationType.NewTour:
                    Title = "A new tour according to your requirements";
                    break;
                default:
                    Title = string.Empty;
                    break;
            }
        }

        public string[] ToCSV()
        {
            string[] retVal = {Id.ToString(), UserId.ToString(), TourId.ToString(), Title, Date.ToString(), Opened.ToString(), NotificationType.ToString()};
            return retVal;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            Title = values[3];
            Date = DateTime.Parse(values[4]);
            Opened = bool.Parse(values[5]);
            NotificationType = Enum.Parse<NotificationType>(values[6]);

            switch (NotificationType)
            {
                case NotificationType.ConfirmPresence:
                    Title = "Confirm your presence";
                    break;
                case NotificationType.TourAccepted:
                    Title = "Tour accepted";
                    break;
                case NotificationType.NewTour:
                    Title = "A new tour according to your requirements";
                    break;
                default:
                    Title = string.Empty;
                    break;
            }
        }
    }
}
