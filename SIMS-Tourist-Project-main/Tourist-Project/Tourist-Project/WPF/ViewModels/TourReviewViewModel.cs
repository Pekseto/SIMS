﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.DTO;
using Tourist_Project.Repositories;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;
using Tourist_Project.WPF.Views;

namespace Tourist_Project.WPF.ViewModels
{
    public class TourReviewViewModel : ViewModelBase
    {
        private readonly TourReviewService ratingService = new();
        private readonly ImageService imageService = new();
        private readonly TourAttendanceService attendanceService = new();
        private readonly TourPointRepository tourPointRepository = new();
        public int KnowledgeRating { get; set; }
        public int LanguageRating { get; set; }
        public int EntertainmentRating { get; set; }
        public string Comment { get; set; }

        private string imageURL;
        public string ImageURL
        {
            get => imageURL;
            set
            {
                if(imageURL != value)
                {
                    imageURL = value;
                    OnPropertyChanged(nameof(ImageURL));
                }
            }
        }
        public List<Image> Images { get; set; }

        public User LoggedInUser { get; set; }
        private readonly NavigationStore navigationStore;
        public TourDTO SelectedTour { get; set; }
        public string Checkpoints { get; set; }

        private Message message;
        public Message Message
        {
            get { return message; }
            set
            {
                if(message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public ICommand RateCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public TourReviewViewModel(User user, TourDTO tour, NavigationStore navigationStore, TourHistoryViewModel previousViewModel)
        {
            LoggedInUser = user;
            SelectedTour = tour;
            Checkpoints = tourPointRepository.GetAllForTourString(SelectedTour.Id);
            this.navigationStore = navigationStore;

            Comment = string.Empty;
            ImageURL = string.Empty;
            Images = new List<Image>();
            Message = new Message();

            RateCommand = new RelayCommand(OnRateClick, CanRate);
            AddCommand = new RelayCommand(OnAddClick, CanAdd);
            BackCommand = new NavigateCommand<TourHistoryViewModel>(this.navigationStore, () => previousViewModel);
        }

        private bool CanAdd()
        {
            return ImageURL != string.Empty;
        }

        private void OnAddClick()
        {
            Image image = new(ImageURL);
            Images.Add(image);
            ImageURL = string.Empty;
        }

        private bool CanRate()
        {
            return KnowledgeRating != 0 && LanguageRating != 0 && EntertainmentRating != 0 && Comment != string.Empty;
        }

        private async Task ShowMessageAndHide(Message message)
        {
            Message = message;
            await Task.Delay(5000);
            Message = new Message();
        }

        private void OnRateClick()
        {
            if(attendanceService.WasUserPresent(LoggedInUser.Id, SelectedTour.Id))
            {
                if(ratingService.DidUserReview(LoggedInUser.Id, SelectedTour.Id))
                {
                    _ = ShowMessageAndHide(new Message(false, "You already reviewed this tour"));
                }
                else
                {
                    TourReview tourReview = new(LoggedInUser.Id, SelectedTour.Id, KnowledgeRating, LanguageRating, EntertainmentRating, Comment);
                    ratingService.Save(tourReview);

                    foreach (var image in Images)
                    {
                        imageService.Save(image);
                    }

                    _ = ShowMessageAndHide(new Message(true, "Successfully rated the tour"));
                }
            }
            else
            {
                _ = ShowMessageAndHide(new Message(false, "You weren't present on this tour, so you can't review it!"));
            }            
        }
    }
}