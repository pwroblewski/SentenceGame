using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ISentenceService, Design.SentenceDesignService>();
            }
            else
            {
                
            }

            SimpleIoc.Default.Register<DomainViewModel>();
            SimpleIoc.Default.Register<LessonsViewModel>();
            SimpleIoc.Default.Register<GamePageViewModel>();
			SimpleIoc.Default.Register<SummaryPageViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DomainViewModel DomainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DomainViewModel>();
            }
        }

        public LessonsViewModel LessonsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LessonsViewModel>();
            }
        }

        public GamePageViewModel GamePageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GamePageViewModel>();
            }
        }

		public SummaryPageViewModel SummaryPageViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SummaryPageViewModel>();
			}
		}

        public ISentenceService DataService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISentenceService>();
            }
        }
    }
}
