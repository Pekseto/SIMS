using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string WayAcquired { get; set; }
        public DateTime LastValidDate { get; set; }

        public Voucher() { }

        public Voucher(int id, int userId, string name, string wayAcquired, DateTime lastValidDate)
        {
            Id = id;
            UserId = userId;
            Name = name;
            WayAcquired = wayAcquired;
            LastValidDate = lastValidDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Name = values[2];
            WayAcquired = values[3];
            LastValidDate = DateTime.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Name, WayAcquired, LastValidDate.ToString() };
            return csvValues;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
