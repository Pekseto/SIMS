using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int CleanlinessGrade { get; set; }
        public int RuleCompliance { get; set; }
        public string Comment { get; set; }
        public GuestRating() { }
        public GuestRating(int reservationId, int cleanlinessGrade, int ruleCompliance, string comment)
        {
            ReservationId = reservationId;
            CleanlinessGrade = cleanlinessGrade;
            RuleCompliance = ruleCompliance;
            Comment = comment;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(), 
                ReservationId.ToString(), 
                CleanlinessGrade.ToString(), 
                RuleCompliance.ToString(), 
                Comment
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            CleanlinessGrade = Convert.ToInt32(values[2]);
            RuleCompliance = Convert.ToInt32(values[3]);
            Comment = values[4];
        }
        //TODO
        public bool IsReviewed()
        {
            return CleanlinessGrade != 0 && RuleCompliance != 0 && !Comment.Equals("");
        }
    }
}