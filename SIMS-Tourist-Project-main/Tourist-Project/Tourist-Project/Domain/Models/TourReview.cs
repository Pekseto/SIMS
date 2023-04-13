using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum ValidStatus
    {
        NotValid, Pending, Valid
    }
    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public int KnowledgeGrade { get; set; }
        public int LanguageGrade { get; set; }
        public int InterestGrade { get; set; }
        public string Comment { get; set; }
        public ValidStatus Valid { get; set; }

        public TourReview()
        {
            
        }

        public TourReview(int id, int userId, int tourId, int knowledgeGrade, int languageGrade, int interestGrade, string comment)
        {
            Id = id;
            UserId = userId;
            TourId = userId;
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            Comment = comment;
            Valid = ValidStatus.NotValid;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                TourId.ToString(),
                KnowledgeGrade.ToString(),
                LanguageGrade.ToString(),
                InterestGrade.ToString(),
                Comment,
                Valid.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            KnowledgeGrade = int.Parse(values[3]);
            LanguageGrade = int.Parse(values[4]);
            InterestGrade = int.Parse(values[5]);
            Comment = values[6];
            Valid = Enum.Parse<ValidStatus>(values[7]);
        }
    }
}
