using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class TourVoucher : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public DateTime ExpireDate { get; set; }

        public TourVoucher() 
        {
        }

        public TourVoucher(int touristsId, int tourId)
        {
            TouristId = touristsId;
            TourId = tourId;
            ExpireDate = DateTime.Now.AddYears(1);
        }

        public string[] ToCSV()
        {
            string[] cssValues =
            {
                Id.ToString(),
                TouristId.ToString(),
                TourId.ToString(),
                ExpireDate.ToString()
            };

            return cssValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            ExpireDate = DateTime.Parse(values[3]);
        }
    }
}
