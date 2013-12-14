using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentenceGame.Portable.ViewModel
{
	public class Word : ViewModelBase
	{
		public string Text { get; set; }

		private bool _IsIncorrect;
		public bool IsIncorrect
		{
			get
			{
				return _IsIncorrect;
			}
			set
			{
				_IsIncorrect = value;
				RaisePropertyChanged(() => IsIncorrect);
			}
		}
	}
}
