using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Serializer;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private const string FilePath = "../../../Data/guests.csv";
        private readonly Serializer<Guest> serializer;
        private List<Guest> guests;

        public GuestRepository()
        {
            serializer = new Serializer<Guest>();
            guests = serializer.FromCSV(FilePath);
        }

        public List<Guest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            guests = serializer.FromCSV(FilePath);
            if(guests.Count < 1)
            {
                return 1;
            }
            return guests.Max(c => c.GuestId) + 1;
        }
        
        public Guest Save(Guest guest)
        {
            guest.GuestId = NextId();
            guests = serializer.FromCSV(FilePath);
            guests.Add(guest);
            serializer.ToCSV(FilePath, guests);
            return guest;
        }

        public void Delete(int id)
        {
            guests = serializer.FromCSV(FilePath);
            var found = guests.Find(C => C.GuestId == id);
            guests.Remove(found);
            serializer.ToCSV(FilePath, guests);
        }

        public Guest Update(Guest guest)
        {
            guests = serializer.FromCSV(FilePath);
            var current = guests.Find(C => C.GuestId == guest.GuestId);
            var index = guests.IndexOf(current);
            guests.Remove(current);
            guests.Insert(index, guest);
            serializer.ToCSV(FilePath, guests);
            return guest;
        }

        public Guest GetOne(int id)
        {
            return guests.Find(C => C.GuestId == id);
        }        
    }
}
