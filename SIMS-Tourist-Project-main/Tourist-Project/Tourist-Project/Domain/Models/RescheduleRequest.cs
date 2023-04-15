using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum RequestStatus { Pending, Confirmed, Declined}
    public class RescheduleRequest : ISerializable
    {
        public DateTime OldBeginningDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public DateTime NewBeginningDate { get; set; }

        private int id;
        public int Id
        {
            get =>id;
            set => id = value;
        }

        private int reservationId;
        public int ReservationId
        {
            get => reservationId;
            set => reservationId = value;
        }

        private RequestStatus status;
        public RequestStatus Status
        {
            get => status;
            set => status = value;
        }

        private string comment;

        public string Comment
        {
            get => comment;
            set => comment = value;
        }
        public RescheduleRequest() { }

        public RescheduleRequest(DateTime oldBeginningDate, DateTime oldEndDate, DateTime newBeginningDate,DateTime newEndDate, int id, int reservationId, RequestStatus status, string comment)
        {
            Id = id;
            OldBeginningDate = oldBeginningDate;
            OldEndDate = oldEndDate;
            NewBeginningDate = newBeginningDate;
            NewEndDate = newEndDate;  
            ReservationId = reservationId;
            Status = status;
            Comment = comment;
        }

        public string[] ToCSV()
        {
           
            string[] csvValues = {
                Id.ToString(),
                OldBeginningDate.ToString(),
                OldEndDate.ToString(),
                NewBeginningDate.ToString(),
                NewEndDate.ToString(),
                ReservationId.ToString(),
                Status.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string []values)
        {
            Id = Convert.ToInt32(values[0]);
            OldBeginningDate = Convert.ToDateTime(values[1]);
            OldEndDate = Convert.ToDateTime(values[2]);
            NewBeginningDate = Convert.ToDateTime(values[3]);
            NewEndDate = Convert.ToDateTime(values[4]);
            ReservationId = Convert.ToInt32(values[5]);
            Status = Enum.Parse<RequestStatus>(values[6]);
            Comment = values[7];
        }


    }
}
