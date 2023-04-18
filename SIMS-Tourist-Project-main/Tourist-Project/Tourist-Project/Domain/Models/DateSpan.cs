using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist_Project.Domain.Models
{
    public class DateSpan
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

    }
}
