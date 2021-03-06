﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SentenceGame.Portable.Helpers;
using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.ViewModel
{
    public class LessonsViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;
        private readonly INavigationService _navigationService;

        #endregion //Fields

        #region Constructor

        public LessonsViewModel(ISentenceService sentenceService, INavigationService navigationService)
        {
            _sentenceService = sentenceService;
            _navigationService = navigationService;

            Messenger.Default.Register<string>(this, LoadData);
        }

        #endregion //Constructor

        #region Properties

        private Domain _domain;
        public Domain DomainProp
        {
            get { return _domain; }
            set { _domain = value; RaisePropertyChanged(() => DomainProp); }
        }

        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get { return _lessons; }
            set { _lessons = value; RaisePropertyChanged(() => Lessons); }
        }

        #endregion //Properties

        #region Commands

        private RelayCommand<Lesson> _navigateToGameCommand;
        public RelayCommand<Lesson> NavigateToGameCommand
        {
            get
            {
                return _navigateToGameCommand
                    ?? (_navigateToGameCommand = new RelayCommand<Lesson>(
                        lesson =>
                        {
                            _navigationService.Navigate("GamePage");
                            Messenger.Default.Send<string, GamePageViewModel>(lesson.LessonPath);
                        }));
            }
        }

    #endregion //Commands

        private async void LoadData(string title)
        {
            DomainProp = await _sentenceService.GetDomain(title);
            Lessons = ExtensionMethods.ToObservableCollection<Lesson>(DomainProp.Lessons);
        }
    }
}
