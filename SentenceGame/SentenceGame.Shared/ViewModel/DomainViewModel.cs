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

namespace SentenceGame.Portable.ViewModel
{
    public class DomainViewModel : ViewModelBase
    {
        #region Fields
        private readonly INavigationService _navigationService;
        private readonly ISentenceService _sentenceService;
        private int _sentenceIndex = 0;

        #endregion //Fields
        #region Constructor

        public DomainViewModel(ISentenceService sentenceService, INavigationService navigationService)
        {
            _sentenceService = sentenceService;
            //_navigationService = navigationService;
            Domains = ExtensionMethods.ToObservableCollection<Domain>(_sentenceService.GetDomain());
            //NextSentence(_sentenceIndex);
        }

        #endregion //Constructor

        #region Properties

        private ObservableCollection<Domain> _domains;
        public ObservableCollection<Domain> Domains
        {
            get { return _domains; }
            set { _domains = value; RaisePropertyChanged(() => Domains); }
        }

        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get { return _lessons; }
            set { _lessons = value; RaisePropertyChanged(() => Lessons); }
        }

        private List<Sentence> _sentences;
        public List<Sentence> Sentences
        {
            get { return _sentences; }
            set { _sentences = value; RaisePropertyChanged(() => Sentences); }
        }

        private Domain _domainObj;
        public Domain DomainObj
        {
            get { return _domainObj; }
            set { _domainObj = value; RaisePropertyChanged(() => DomainObj); }
        }

        private string _answer;
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; RaisePropertyChanged(() => Answer); }
        }

        private ObservableCollection<string> _translation;
        public ObservableCollection<string> Translation
        {
            get { return _translation; }
            set { _translation = value; RaisePropertyChanged(() => Translation); }
        }

        private ObservableCollection<string> _selTranslation;
        public ObservableCollection<string> SelTranslation
        {
            get { return _selTranslation; }
            set { _selTranslation = value; RaisePropertyChanged(() => SelTranslation); }
        }

        private ObservableCollection<string> _goodTranslation;
        public ObservableCollection<string> GoodTranslation
        {
            get { return _goodTranslation; }
            set { _goodTranslation = value; RaisePropertyChanged(() => GoodTranslation); }
        }

        #endregion //Properties

        #region Commands

        private RelayCommand<string> _selectCommand;
        public RelayCommand<string> SelectCommand
        {
            get
            {
                return _selectCommand
                    ?? (_selectCommand = new RelayCommand<string>(
                        word =>
                        {
                            Translation.Remove(word);
                            SelTranslation.Add(word.ToString());
                            if (SelTranslation.SequenceEqual(GoodTranslation))
                            {
                                Answer = "Correct";
                                _sentenceIndex++;
                                NextSentence(_sentenceIndex);
                            }
                        }));
            }
        }

        private RelayCommand<string> _selectRevCommand;
        public RelayCommand<string> SelectRevCommand
        {
            get
            {
                return _selectRevCommand
                    ?? (_selectRevCommand = new RelayCommand<string>(
                        word =>
                        {
                            SelTranslation.Remove(word);
                            Translation.Add(word.ToString());
                            Answer = "";
                        }));
            }
        }

        private RelayCommand _navigateToLessonsCommand;
        public RelayCommand NavigateToLessonsCommand
        {
            get
            {
                return _navigateToLessonsCommand
                    ?? (_navigateToLessonsCommand = new RelayCommand(
                        () =>
                        {
                            _navigationService.Navigate("LessonsPage");
                        }));
            }
        }



        #endregion //Commands

        #region Methods

        private void NextSentence(int sentenceIndex)
        {
            //if (Sentences.Count > sentenceIndex)
            //{
            //    Sentence = Sentences[sentenceIndex];
            //    var list = Sentence.Translation.Split(' ').ToList<string>();
            //    var list2 = list.OrderBy(a => Guid.NewGuid());
            //    Translation = ExtensionMethods.ToObservableCollection<string>(list2);
            //    GoodTranslation = ExtensionMethods.ToObservableCollection<string>(list);
            //    SelTranslation = new ObservableCollection<string>();
            //    Answer = "";
            //}
        }

        #endregion //Methods
    }
}
