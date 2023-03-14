using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Model
{
	public class GuestReview : ISerializable
    {
		public int Id { get; set; }
		public User owner { get; set; }
		public User guest { get; set; }
		public int cleanlinessGrade { get; set; }
		public int ruleGrade { get; set; }
		public string comment { get; set; }
		public GuestReview() { }
        public GuestReview(User owner, User guest, int cleanlinessGrade, int ruleGrade, string comment)
        {
            this.owner = owner;
            this.guest = guest;
            this.cleanlinessGrade = cleanlinessGrade;
            this.ruleGrade = ruleGrade;
            this.comment = comment;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), owner.Id.ToString(), guest.Id.ToString(), cleanlinessGrade.ToString(), ruleGrade.ToString(), comment};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            owner = new User() { Id = Convert.ToInt32(values[1])};
            guest = new User() { Id = Convert.ToInt32(values[2])};
            cleanlinessGrade = Convert.ToInt32(values[3]);
            ruleGrade = Convert.ToInt32(values[4]);
            comment = values[5];
        }
        public bool IsReviewed()
        {
            return cleanlinessGrade != 0 && ruleGrade != 0 && comment != null;
        }
    }
}