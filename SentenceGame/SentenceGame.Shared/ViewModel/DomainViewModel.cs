using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SentenceGame.Portable.Helpers;
using SentenceGame.Portable.Design;
using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;

namespace SentenceGame.Portable.ViewModel
{
    public class DomainViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private readonly ISentenceService _sentenceService;

        #endregion //Fields

        #region Constructor

        public DomainViewModel(ISentenceService sentenceService, INavigationService navigationService)
        {
            _sentenceService = sentenceService;
            _navigationService = navigationService;

            LoadDomains();
        }

        #endregion //Constructor

        #region Properties

        private ObservableCollection<Domain> _domains;
        public ObservableCollection<Domain> Domains
        {
            get { return _domains; }
            set { _domains = value; RaisePropertyChanged(() => Domains); }
        }

        #endregion //Properties

        #region Commands

        private RelayCommand<string> _navigateToLessonsCommand;
        public RelayCommand<string> NavigateToLessonsCommand
        {
            get
            {
                return _navigateToLessonsCommand
                    ?? (_navigateToLessonsCommand = new RelayCommand<string>(
                        title =>
                        {
                            _navigationService.Navigate("LessonsPage");
                            Messenger.Default.Send<string, LessonsViewModel>(title);
                        }));
            }
        }

        private RelayCommand<string> _navigateToGameCommand;
        public RelayCommand<string> NavigateToGameCommand
        {
            get
            {
                return _navigateToGameCommand
                    ?? (_navigateToGameCommand = new RelayCommand<string>(
                        title =>
                        {
                            _navigationService.Navigate("GamePage");
                            Messenger.Default.Send<string, GamePageViewModel>(title);
                        }));
            }
        }

        #endregion //Commands

        #region Methods

        private async Task LoadDomains()
        {
            Domains = ExtensionMethods.ToObservableCollection<Domain>(await _sentenceService.GetDomains());
        }

        #endregion //Methods
    }
}
