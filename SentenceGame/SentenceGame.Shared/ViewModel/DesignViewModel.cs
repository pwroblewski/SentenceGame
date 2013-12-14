using GalaSoft.MvvmLight;
using SentenceGame.Portable.Design;
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
    public class DesignViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISentenceService _sentenceService;

        #endregion //Fields

        #region Constructor

        public DesignViewModel()
        {
            if (IsInDesignMode)
            {
                _sentenceService = new SentenceDesignService();
                LoadData();
            }
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

        #endregion //Properties

        private async Task LoadData()
        {
            Domains = ExtensionMethods.ToObservableCollection<Domain>(await _sentenceService.GetDomains());
            //Lessons = Domains[0].Lessons;
        }
    }
}
