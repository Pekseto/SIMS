﻿using System.Collections.Generic;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Applications.UseCases
{
    public class LocationService : IService<Location>
    {
        private static readonly Injector injector = new();

        private readonly ILocationRepository locationRepository =
            injector.CreateInstance<ILocationRepository>();

        public LocationService() 
        {
        }

        public Location Create(Location location)
        {
            return locationRepository.Save(location);
        }

        public List<Location> GetAll()
        {
            return locationRepository.GetAll();
        }

        public Location Get(int id)
        {
            return locationRepository.GetById(id);
        }

        public Location Update(Location location)
        {
            return locationRepository.Update(location);
        }

        public void Delete(int id)
        {
            locationRepository.Delete(id);
        }

        public int GetId(string city, string country)
        {
            return locationRepository.GetId(city, country);
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
    }
}
