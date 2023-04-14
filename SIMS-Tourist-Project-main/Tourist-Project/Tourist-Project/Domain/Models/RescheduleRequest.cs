using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class RescheduleRequest : ISerializable
    {
        public DateTime OldBegginigDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public DateTime NewBegginigDate { get; set; }

        private int _rescheduleRequestId;

        private int _reservationId;
        public int ReservationId
        {
            get => _reservationId;
            set => _reservationId = value;
        }

        public int RescheduleRequestId
        {
            get =>_rescheduleRequestId;
            set => _rescheduleRequestId = value;
        }

        public RescheduleRequest() { }

        public RescheduleRequest(DateTime oldBegginigDate, DateTime oldEndDate, DateTime newBegginigDate,DateTime newEndDate, int rescheduleRequestId, int reservationId)
        {
            RescheduleRequestId = rescheduleRequestId;
            OldBegginigDate = oldBegginigDate;
            OldEndDate = oldEndDate;
            NewBegginigDate = newBegginigDate;
            NewEndDate = newEndDate;  
            ReservationId = reservationId;
        }

        public string[] ToCSV()
        {
           
            string[] csvValues = {
                RescheduleRequestId.ToString(),
                OldBegginigDate.ToString(),
                OldEndDate.ToString(),
                NewBegginigDate.ToString(),
                NewEndDate.ToString(),
                ReservationId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string []values)
        {
            RescheduleRequestId = Convert.ToInt32(values[0]);
            OldBegginigDate = Convert.ToDateTime(values[1]);
            OldEndDate = Convert.ToDateTime(values[2]);
            NewBegginigDate = Convert.ToDateTime(values[3]);
            NewEndDate = Convert.ToDateTime(values[4]);
            ReservationId = Convert.ToInt32(values[5]);
        }


    }
}
