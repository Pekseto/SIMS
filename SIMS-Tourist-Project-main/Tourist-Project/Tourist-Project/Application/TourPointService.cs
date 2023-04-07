using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Repository;
using Tourist_Project.View;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.Application
{
    public class TourPointService
    {
        private readonly TourPointRepository repository = new();
        public event EventHandler RequestClose;

        public TourPoint GetOne(int id)
        {
            return repository.GetOne(id);
        }

        public void UpdateCollection(TourPoint selectedTourPoint, Tour selectedTour)
        {
            repository.Update(selectedTourPoint);
            TourLiveViewModel.TourPoints.Clear();
            foreach (TourPoint point in repository.GetAllForTour(selectedTour.Id))
            {
                TourLiveViewModel.TourPoints.Add(point);
            }
        }

        public void EndTour()
        {
            if (IsAllChecked()) {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsAllChecked()
        {
            return TourLiveViewModel.TourPoints.ToList().Find(tourPoint => tourPoint.Visited == false) == null;
        }

    }
}
