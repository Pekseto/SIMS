using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class GuideProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Years { get; set; } = new();
        private readonly TourService tourService = new ();
        private Window window;
        public Tour Tour { get; set; }

        private string selectedYear;
        public string SelectedYear
        {
            get { return selectedYear; }
            set
            {
                selectedYear = value;
                OnPropertyChanged("SelectedYear");
                BestTourInfo();
            }
        }

        #region IPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand HomeViewCommand { get; set; }
        public ICommand StatisticsViewCommand { get; set; }
        public GuideProfileViewModel(Window window)
        {
            this.window = window;

            HomeViewCommand = new RelayCommand(HomeView, CanHomeView);
            StatisticsViewCommand = new RelayCommand(StatisticsView, CanStatisticsView);

            InitializeYears();
            SelectedYear = "2022";
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

        private void BestTourInfo()
        {
            if (!SelectedYear.Equals("Overall"))
            {
                Tour = tourService.GetMostVisited(Int32.Parse(SelectedYear));
            }
            else
            {
                Tour = tourService.GetOverallBest();
            }
            OnPropertyChanged("Tour");
        }
    }
}
