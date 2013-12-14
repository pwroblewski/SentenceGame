using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
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
	public class SummaryPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;
        private readonly INavigationService _navigationService;

        #endregion //Fields

        #region Constructor

        public SummaryPageViewModel(ISentenceService sentenceService, INavigationService navigationService)
        {
            _sentenceService = sentenceService;
            _navigationService = navigationService;

			Correct = 3;
			Incorrect = 5;
        }

        #endregion //Constructor

        #region Properties

        private int _correct;
        public int Correct
        {
            get { return _correct; }
            set { _correct = value; RaisePropertyChanged(() => Correct); }
        }

		 private int _inCorrect;
        public int Incorrect
        {
            get { return _inCorrect; }
			set { _inCorrect = value; RaisePropertyChanged(() => Incorrect); }
        }

		public double CorrectWidth
		{
			get
			{
				return (double)(((double)_correct / (_correct + _inCorrect)) * 700); // 700 point == 100%
			}
		}

		public double IncorrectWidth
		{
			get
			{
				return (double)(((double)_inCorrect / (_correct + _inCorrect)) * 700); // 700 point == 100%
			}
		}

        #endregion //Properties
	}
}
