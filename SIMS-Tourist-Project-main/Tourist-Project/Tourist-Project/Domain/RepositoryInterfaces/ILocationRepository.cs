using System.Collections.Generic;
using Microsoft.Win32;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        public List<Location> GetAll();
        public int GetId(string city, string country);
        public Location GetById(int id);
        public Location Save(Location location);
        public Location Update(Location location);
        public void Delete(int id);
    }
}
