using GalaSoft.MvvmLight;
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
        private int _sentenceIndex = 0;

        #endregion //Fields
        #region Constructor

        public LessonsViewModel(ISentenceService sentenceService)
        {
            _sentenceService = sentenceService;
            //Lessons = _sentenceService.GetLessons(domain);
            //NextSentence(_sentenceIndex);
        }

        #endregion //Constructor

        #region Properties

        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get { return _lessons; }
            set { _lessons = value; RaisePropertyChanged(() => Lessons); }
        }

        #endregion //Properties
    }
}
