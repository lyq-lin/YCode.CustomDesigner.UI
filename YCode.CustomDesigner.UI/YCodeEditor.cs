using System.Collections;

using System.Windows.Controls.Primitives;

namespace YCode.CustomDesigner.UI
{
	public class YCodeEditor : MultiSelector
	{
		static YCodeEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(YCodeEditor), new FrameworkPropertyMetadata(typeof(YCodeEditor)));
		}

		private readonly YCodeSourceManager _manager;

		public YCodeEditor()
		{
			_manager = new YCodeSourceManager(this);
		}

		#region Denpendercy Property

		public IEnumerable Nodes
		{
			get { return (IEnumerable)GetValue(NodesProperty); }
			set { SetValue(NodesProperty, value); }
		}

		public IEnumerable Lines
		{
			get { return (IEnumerable)GetValue(LinesProperty); }
			set { SetValue(LinesProperty, value); }
		}

		public YCodeSourceViewModel Source
		{
			get { return (YCodeSourceViewModel)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(YCodeSourceViewModel), typeof(YCodeEditor));

		public static readonly DependencyProperty LinesProperty =
			DependencyProperty.Register("Lines", typeof(IEnumerable), typeof(YCodeEditor));

		public static readonly DependencyProperty NodesProperty =
			DependencyProperty.Register("Nodes", typeof(IEnumerable), typeof(YCodeEditor));

		#endregion

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);

			if (e.Property == SourceProperty)
			{
				_manager.OnChanged();
			}
		}
	}
}
