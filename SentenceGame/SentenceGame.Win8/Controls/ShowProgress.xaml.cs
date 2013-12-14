using SentenceGame.Portable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SentenceGame.Win8
{
    public sealed partial class ShowProgress : UserControl
    {
		public int CurrentIndex
		{
			get { return (int)GetValue(CurrentIndexProperty); }
			set { SetValue(CurrentIndexProperty, value); }
		}

		public static readonly DependencyProperty CurrentIndexProperty =
			DependencyProperty.Register("CurrentIndex", typeof(int), typeof(ShowProgress), new PropertyMetadata(0, new PropertyChangedCallback(OnCurrentIndexChanged)));

		public ObservableCollection<Sentence> Sentences
		{
			get { return (ObservableCollection<Sentence>)GetValue(SentencesProperty); }
			set { SetValue(SentencesProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Sentences.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SentencesProperty =
			DependencyProperty.Register("Sentences", typeof(ObservableCollection<Sentence>), typeof(ShowProgress), new PropertyMetadata(new ObservableCollection<Sentence>(), new PropertyChangedCallback(OnSentencesChanged)));


		public Thickness TickMargin
		{
			get { return (Thickness)GetValue(TickMarginProperty); }
			set { SetValue(TickMarginProperty, value); }
		}

		public static readonly DependencyProperty TickMarginProperty =
			DependencyProperty.Register("TickMargin", typeof(int), typeof(ShowProgress), new PropertyMetadata(default(Thickness)));


		public Thickness CurrentTickMargin
		{
			get { return (Thickness)GetValue(CurrentTickMarginProperty); }
			set { SetValue(CurrentTickMarginProperty, value); }
		}

		public static readonly DependencyProperty CurrentTickMarginProperty =
			DependencyProperty.Register("CurrentTickMargin", typeof(int), typeof(ShowProgress), new PropertyMetadata(default(Thickness)));


        public ShowProgress()
        {
            this.InitializeComponent();
        }

		private static void OnCurrentIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{					 
			var ctrl = d as ShowProgress;
			var highlightedIndex = (int)e.NewValue;

			CalculateCurrentSentenceHighlight(ctrl, highlightedIndex);
		}

		private static void OnSentencesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var ctrl = d as ShowProgress;
			var sentences = e.NewValue as ObservableCollection<Sentence>;
			sentences.CollectionChanged += (sender, arg) =>
														{
															CalculateTicksSpace(ctrl, sentences);
														};

			CalculateTicksSpace(ctrl, sentences);
		}

		private static void CalculateTicksSpace(ShowProgress ctrl, ObservableCollection<Sentence> sentences)
		{
			if (sentences.Count > 0)
			{
				ctrl.TickMargin = new Thickness((int)(ctrl.ActualWidth / (sentences.Count + 1)), 0, 0, 0);
				CalculateCurrentSentenceHighlight(ctrl, ctrl.CurrentIndex);
			}
		}

		private static void CalculateCurrentSentenceHighlight(ShowProgress ctrl, int highlightedIndex)
		{
			var space = (int)(ctrl.ActualWidth / (ctrl.Sentences.Count + 1));
			var newX = (space + 5) * (highlightedIndex + 1);

			var move = new DoubleAnimation()
								{
									To = (int)newX,
									Duration = TimeSpan.FromMilliseconds(400),
									EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut }
								};

			Storyboard.SetTarget(move, ctrl.currentProgressIndicator);
			Storyboard.SetTargetProperty(move, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
			
			Storyboard b = new Storyboard();
			b.Children.Add(move);
			b.Begin();

			//ctrl.CurrentTickMargin = new Thickness((int)newX , 0, 0, 0);
		}
    }
}
