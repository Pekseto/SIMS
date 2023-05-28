using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Applications.UseCases
{
    public class ComplexTourService
    {
        private static readonly Injector injector = new();
        private readonly IComplexTourRepository complexTourRepository = injector.CreateInstance<IComplexTourRepository>();
        public ComplexTourService()
        {
        }

        public List<ComplexTour> GetAll()
        {
            return complexTourRepository.GetAll();
        }

        public List<ComplexTour> GetAllForUser(int userId)
        {
            return complexTourRepository.GetAllForUser(userId);
        }

        public ComplexTour Save(ComplexTour complexTour)
        {
            return complexTourRepository.Save(complexTour);
        }
    }
}
