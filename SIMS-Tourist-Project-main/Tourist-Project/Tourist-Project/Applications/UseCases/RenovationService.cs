using System.Collections.Generic;
using Tourist_Project.Domain;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class RenovationService
    {
        private static readonly Injector injector = new();

        private readonly IRenovationRepository renovationRepository = injector.CreateInstance<IRenovationRepository>();
        public RenovationService() { }

        public Renovation Create(Renovation renovation)
        {
            return renovationRepository.Save(renovation);
        }

        public List<Renovation> GetAll()
        {
            return renovationRepository.GetAll();
        }

        public Renovation Get(int id)
        {
            return renovationRepository.GetById(id);
        }
        public Renovation Update(Renovation renovation)
        {
            return renovationRepository.Update(renovation);
        }

        public void Delete(int id)
        {
            renovationRepository.Delete(id);
        }
    }

}