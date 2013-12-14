using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SentenceGame.Portable.Helpers;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace SentenceGame.Portable.ViewModel
{
    public class GamePageViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;
        private readonly INavigationService _navigationService;

        private int _sentenceIndex = 0;

        #endregion //Fields

        #region Constructor

        public GamePageViewModel(ISentenceService sentenceService, INavigationService navigationService)
        {
            _sentenceService = sentenceService;
            _navigationService = navigationService;

            Messenger.Default.Register<string>(this, LoadData);
        }

        #endregion //Constructor

        #region Properties

		public int SentenceIndex
		{
			get { return _sentenceIndex; }
			set { _sentenceIndex = value; RaisePropertyChanged(() => SentenceIndex); }
		}


        private ObservableCollection<Sentence> _sentences;
        public ObservableCollection<Sentence> Sentences
        {
            get { return _sentences; }
            set { _sentences = value; RaisePropertyChanged(() => Sentences); }
        }

        private Sentence _sentence;
        public Sentence Sentence
        {
            get { return _sentence; }
			set { _sentence = value; RaisePropertyChanged(() => Sentence);  }
        }

        private string _answer;
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; RaisePropertyChanged(() => Answer); }
        }

		private bool _isCorrect;
		public bool IsCorrect
		{
			get { return _isCorrect; }
			set { _isCorrect = value; RaisePropertyChanged(() => IsCorrect); }
		}

		private bool _isIncorrect;
		public bool IsIncorrect
		{
			get { return _isIncorrect; }
			set { _isIncorrect = value; RaisePropertyChanged(() => IsIncorrect); }
		}

        private ObservableCollection<string> _translation;
		public ObservableCollection<string> Translation
        {
            get { return _translation; }
            set { _translation = value; RaisePropertyChanged(() => Translation); }
        }

		private ObservableCollection<Word> _selTranslation;
		public ObservableCollection<Word> SelTranslation
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
							SelTranslation.Add(new Word() { Text = word });

							if (Translation.Count == 0)
							{
								for (int i = 0; i < GoodTranslation.Count; i++)
								{
									if (GoodTranslation[i] != SelTranslation[i].Text)
										SelTranslation[i].IsIncorrect = true;
								}

								bool translationFailed = SelTranslation.Any(w => w.IsIncorrect);

								if (translationFailed)
									IsIncorrect = true;
								else
									IsCorrect = true;
							}
                        }));
            }
        }

        private RelayCommand<Word> _selectRevCommand;
		public RelayCommand<Word> SelectRevCommand
        {
            get
            {
                return _selectRevCommand
					?? (_selectRevCommand = new RelayCommand<Word>(
                        word =>
                        {
							if (IsCorrect || IsIncorrect)
								return; // so that user doesn't mess up logic

                            SelTranslation.Remove(word);
							Translation.Add(word.Text);
                            Answer = "";
                        }));
            }
        }

		private RelayCommand _nextCommand;
		public RelayCommand NextCommand
		{
			get
			{
				return _nextCommand
					?? (_nextCommand = new RelayCommand(
						async () =>
						{
							IsCorrect = false;
							IsIncorrect = false;

							bool translationFailed = SelTranslation.Any(w => w.IsIncorrect);
							await NextSentence(++SentenceIndex, !translationFailed);
						}));
			}
		}

        #endregion //Commands

        #region Methods

        private async void LoadData(string lessonPath)
        {
			_isCorrect = false;
			_isIncorrect = false;
            var sentences = await _sentenceService.GetSentences(lessonPath);
            Sentences = ExtensionMethods.ToObservableCollection<Sentence>(sentences);

            SentenceIndex = 0;
            await NextSentence(SentenceIndex, true);
        }

        private async Task NextSentence(int sentenceIndex, bool answer)
        {
			if (sentenceIndex > 0)
				Sentences[sentenceIndex - 1].IsCorrect = answer;

            if (!answer)
            {
				var prev = Sentences[sentenceIndex - 1];
				Sentences.Add(new Sentence()
									{
										ImagePath = prev.ImagePath,
										Text = prev.Text,
										Translation = prev.Translation
									});
            }

            if (Sentences.Count > sentenceIndex)
            {
                Sentence = Sentences[sentenceIndex];
                var list = Sentence.Translation.Split(' ').ToList<string>();
                var list2 = list.OrderBy(a => Guid.NewGuid());
                Translation = ExtensionMethods.ToObservableCollection<string>(list2);
                GoodTranslation = ExtensionMethods.ToObservableCollection<string>(list);
                SelTranslation = new ObservableCollection<Word>();
                Answer = "";
            }
            else
            {	  
                await DialogService.ShowMessage("Gratulacje !!! Poprawnie ułożono wszystkie zdania.", "Gra skończona");
                _navigationService.GoBack();
            }
        }

        #endregion //Methods

        #region Dialog Service
        public IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDialogService>();
            }
        }
        #endregion
    }
}
