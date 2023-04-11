using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{ 
    public class GuestRateService : IService<GuestRating>
    {
        private static readonly Injector injector = new();

        private readonly IGuestRateRepository guestRateRepository =
            injector.CreateInstance<IGuestRateRepository>();
        public GuestRateService()
        {
        }
        public GuestRating Create(GuestRating guestRating)
        {
            return guestRateRepository.Save(guestRating);
        }
        public List<GuestRating> GetAll()
        {
            return guestRateRepository.GetAll();
        }

        public GuestRating Get(int id)
        {
            return guestRateRepository.GetById(id);
        }
        public GuestRating Update(GuestRating guestRating)
        {
            return guestRateRepository.Update(guestRating);
        }
        public void Delete(int id)
        {
            guestRateRepository.Delete(id);
        }
    }

}