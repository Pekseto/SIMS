using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        public List<TourRequest> GetAll();
        public TourRequest Save(TourRequest tourRequest);
        public int NextId();
        public TourRequest Update(TourRequest tourRequest);
        public void Delete(int id);
        public TourRequest GetById(int id);
        List<TourRequest> GetAllForUser(int userId);
        List<int> GetAllRequestedLocations(int userId);
        List<string> GetAllRequestedLanguages(int userId);
    }
}
