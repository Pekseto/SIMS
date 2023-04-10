﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Applications.UseCases
{
    public class TourAttendanceService
    {
        private static readonly Injector injector = new();

        private readonly ITourAttendanceRepository repository = injector.CreateInstance<ITourAttendanceRepository>();

        private UserService userService = new();
        private TourPointService tourPointService = new();

        public TourAttendanceService()
        {
        }

        public List<TourAttendance> GetAllTourists(TourPoint selectedTourPoint)
        {
            return repository.GetAllTourists(selectedTourPoint);
        }

        public void UpdateCollection(TourAttendance selectedTourAttendance, TourPoint selectedTourPoint)
        {
            var tourAttendances = TouristListViewModel.TourAttendances;
            repository.Update(selectedTourAttendance);
            tourAttendances.Clear();
            foreach (TourAttendance attendance in GetAllTourists(selectedTourPoint))
            {
                tourAttendances.Add(attendance);
            }
            foreach (TourAttendance attendace in tourAttendances)
            {
                attendace.User = userService.GetOne(attendace.UserId);
                attendace.TourPoint = tourPointService.GetOne(attendace.CheckPointId);
            }
        }
    }
}
