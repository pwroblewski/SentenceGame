using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace SentenceGame.Win8.Helpers
{
	/// <summary>
	/// This attached property binds to a bool and a pair of StoryboardAnimations
	/// When the bool is true, the true animation is started
	/// When the bool is false, the fals animation is started
	/// </summary>
	public class BoolBeginStoryBoardTrigger
	{
		#region Bool Property

		public static readonly DependencyProperty BoolProperty =
		  DependencyProperty.RegisterAttached("Bool", typeof(bool), typeof(BoolBeginStoryBoardTrigger), new PropertyMetadata(false, BoolPropertyChanged));

		public static void SetBool(DependencyObject attached, bool value)
		{
			attached.SetValue(BoolProperty, value);
		}

		public static bool GetBool(DependencyObject attached)
		{
			return (bool)attached.GetValue(BoolProperty);
		}

		private static void BoolPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (GetBool(d))
				GetTrueStoryboard(d).Begin();
			else
				GetFalseStoryboard(d).Begin();
		}

		#endregion

		#region TrueStoryboard Property

		public static readonly DependencyProperty TrueStoryboardProperty =
		  DependencyProperty.RegisterAttached("TrueStoryboard", typeof(Storyboard), typeof(BoolBeginStoryBoardTrigger), null);

		public static void SetTrueStoryboard(DependencyObject attached, Storyboard value)
		{
			attached.SetValue(TrueStoryboardProperty, value);
		}

		public static Storyboard GetTrueStoryboard(DependencyObject attached)
		{
			return (Storyboard)attached.GetValue(TrueStoryboardProperty);
		}

		#endregion

		#region FalseStoryboard Property

		public static readonly DependencyProperty FalseStoryboardProperty =
		DependencyProperty.RegisterAttached("FalseStoryboard", typeof(Storyboard), typeof(BoolBeginStoryBoardTrigger), null);

		public static void SetFalseStoryboard(DependencyObject attached, Storyboard value)
		{
			attached.SetValue(FalseStoryboardProperty, value);
		}

		public static Storyboard GetFalseStoryboard(DependencyObject attached)
		{
			return (Storyboard)attached.GetValue(FalseStoryboardProperty);
		}

		#endregion
	}

}
