using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuideProfileViewModel
    {
        private Window window;
        public Tour Tour { get; set; }
        public string SelectedYear { get; set; }

        public ObservableCollection<string> Years { get; set; } = new();

        public ICommand HomeViewCommand { get; set; }
        public ICommand StatisticsViewCommand { get; set; }
        public GuideProfileViewModel(Window window)
        {
            this.window = window;

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);

            InitializeYears();
            SelectedYear = "Overall";

            
        }

        private bool CanHomeView()
        {
            return true;
        }

        private void HomeView()
        {
            var homeWindow = new TodayToursView();
            homeWindow.Show();
            window.Close();
        }

        private bool CanStatisticsView()
        {
            return true;
        }

        private void StatisticsView()
        {
            var statisticsWindow = new StatisticsOfTourView(Tour);
            statisticsWindow.Show();
            window.Close();
        }

        private void InitializeYears()
        {
            var startYear = DateTime.Now.Year;
            var endYear = startYear - 50;
            Years.Add("Overall");
            for (var year = startYear; year >= endYear; year--)
            {
                Years.Add(year.ToString());
            }
        }
    }
}
