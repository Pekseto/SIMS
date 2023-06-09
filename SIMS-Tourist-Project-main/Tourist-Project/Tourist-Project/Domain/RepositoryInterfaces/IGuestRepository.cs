using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IGuestRepository
    {
        public List<Guest> GetAll();
        public Guest GetOne(int id);
        public int NextId();
        public void Delete(int id);
        public Guest Update( Guest guest);
        public Guest Save(Guest guest);
        

    }
}
