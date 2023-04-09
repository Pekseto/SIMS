using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        public List<TourReservation> GetAll();
        public void Save(TourReservation reservation);
        public int NextId();
    }
}
