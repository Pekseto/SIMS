using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class DateSpan : ISerializable
    {
        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public DateSpan()
        {
        }

        public DateSpan(DateTime startingDate, DateTime endingDate)
        {
            StartingDate = startingDate;
            EndingDate = endingDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                StartingDate.ToString("MM/dd/yyyy"),
                EndingDate.ToString("MM/dd/yyyy")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            StartingDate = DateTime.Parse(values[1]);
            EndingDate = DateTime.Parse(values[2]);
        }
    }
}