using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;

        #endregion //Fields

        #region Constructor

        public MainViewModel(ISentenceService sentenceService)
        {
            _sentenceService = sentenceService;
            Sentences = _sentenceService.GetSentences();
            Sentence = Sentences[0];
            Translation = Sentence.Translation.Split(' ').ToList<string>();
        }

        #endregion //Constructor

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

        private List<string> _translation;
        public List<string> Translation
        {
            get { return _translation; }
            set { _translation = value; RaisePropertyChanged(() => Translation); }
        }

        //private RelayCommand _startGame;
        //public RelayCommand StartGame
        //{
        //    get
        //    {
        //        return _startGame
        //            ?? (_startGame = new RelayCommand(
        //                () =>
        //                {
                           
        //                }));
        //    }
        //}
    }
}
