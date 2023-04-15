using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Date
    {
        public DateTime OneDate { get; set; }
        public bool IsFree { get; set; }

        public Date()
        {

        }
        public Date(DateTime dateTime, bool isFree)
        {
            OneDate = dateTime;
            IsFree = isFree;
        }

    }
}
