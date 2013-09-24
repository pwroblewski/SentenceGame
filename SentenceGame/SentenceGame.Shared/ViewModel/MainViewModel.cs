using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SentenceGame.Portable.Helpers;

namespace SentenceGame.Portable.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;
        private int _sentenceIndex = 0;

        #endregion //Fields

        #region Constructor

        public MainViewModel(ISentenceService sentenceService)
        {
            _sentenceService = sentenceService;
            Sentences = _sentenceService.GetSentences();

            NextSentence(_sentenceIndex);
        }

        #endregion //Constructor

        #region Properties

        private List<Sentence> _sentences;
        public List<Sentence> Sentences
        {
            get { return _sentences; }
            set { _sentences = value; RaisePropertyChanged(() => Sentences); }
        }

        private Sentence _sentence;
        public Sentence Sentence
        {
            get { return _sentence; }
            set { _sentence = value; RaisePropertyChanged(() => Sentence); }
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

        #endregion //Commands

        #region Methods

        private void NextSentence(int sentenceIndex)
        {
            if (Sentences.Count > sentenceIndex)
            {
                Sentence = Sentences[sentenceIndex];
                var list = Sentence.Translation.Split(' ').ToList<string>();
                var list2 = list.OrderBy(a => Guid.NewGuid());
                Translation = ExtensionMethods.ToObservableCollection<string>(list2);
                GoodTranslation = ExtensionMethods.ToObservableCollection<string>(list);
                SelTranslation = new ObservableCollection<string>();
                Answer = "";
            }
        }

        #endregion //Methods
    }
}
