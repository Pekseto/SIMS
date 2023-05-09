using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class MyTourPreviewViewModel : ViewModelBase
    {
        private readonly TourPointRepository tourPointRepository = new();
        public TourDTO SelectedTour { get; set; }
        public string Checkpoints { get; set; }
        public ICommand BackCommand {  get; set; }
        public MyTourPreviewViewModel(TourDTO tour, NavigationStore navigationStore, MyToursViewModel previousViewModel)
        {
            SelectedTour = tour;
            Checkpoints = tourPointRepository.GetAllForTourString(tour.Id);

            BackCommand = new NavigateCommand<MyToursViewModel>(navigationStore, () => previousViewModel);           
        }
    }
}
