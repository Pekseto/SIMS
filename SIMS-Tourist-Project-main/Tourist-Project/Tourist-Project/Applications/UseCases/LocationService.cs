using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Application
{
    public class LocationService
    {
        private readonly LocationRepository repository = new();

        public LocationService() 
        {
        }

        public List<Location> GetAll()
        {
            return repository.GetAll();
        }

        public int GetId(string city, string country)
        {
            return repository.GetId(city, country);
        }

        public void InitializeCitiesAndCountries()
        {
            foreach (var location in GetAll())
            {
                CreateTourViewModel.Cities.Add(location.City);
                if (!CreateTourViewModel.Countries.Contains(location.Country))
                    CreateTourViewModel.Countries.Add(location.Country);
            }
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new();
            foreach (var location in GetAll())
            {
                cities.Add(location.City);
            }
            return cities;
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new();
            foreach (var location in GetAll())
            {
                if (!countries.Contains(location.Country))
                {
                    countries.Add(location.Country);
                }
            }
            return countries;
        }
    }
}
