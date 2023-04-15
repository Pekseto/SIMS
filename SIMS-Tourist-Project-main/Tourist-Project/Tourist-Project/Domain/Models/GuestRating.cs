using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public int CleanlinessGrade { get; set; }
        public int RuleGrade { get; set; }
        public string Comment { get; set; }
        public GuestRating() { }
        public GuestRating(int ownerId, int guestId, int cleanlinessGrade, int ruleGrade, string comment)
        {
            OwnerId = ownerId;
            GuestId = guestId;
            CleanlinessGrade = cleanlinessGrade;
            RuleGrade = ruleGrade;
            Comment = comment;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString(), GuestId.ToString(), CleanlinessGrade.ToString(), RuleGrade.ToString(), Comment };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            GuestId = Id = Convert.ToInt32(values[2]);
            CleanlinessGrade = Convert.ToInt32(values[3]);
            RuleGrade = Convert.ToInt32(values[4]);
            Comment = values[5];
        }
        public bool IsReviewed()
        {
            return CleanlinessGrade != 0 && RuleGrade != 0 && !Comment.Equals("");
        }
    }
}