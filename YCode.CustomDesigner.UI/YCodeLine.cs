using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace YCode.CustomDesigner.UI
{
	public class YCodeLine : Shape
	{
		private YCodeCanvas? _canvas;
		private readonly PathFigure _figure;
		private readonly BezierSegment _segment;

		private bool _isLoaded;

		public YCodeLine()
		{
			_figure = new PathFigure();
			_segment = new BezierSegment();
			_figure.Segments.Add(_segment);

			this.DefiningGeometry = new PathGeometry(new PathFigure[] { _figure });

			this.StrokeThickness = 2;

			this.Stroke = Brushes.Purple;

			Loaded += this.OnLoaded;
		}

		internal YCodeNode Source { get; set; } = default!;
		internal YCodeNode Target { get; set; } = default!;

		#region Dependency Property

		public string SourceId
		{
			get { return (string)GetValue(SourceIdProperty); }
			set { SetValue(SourceIdProperty, value); }
		}

		public static readonly DependencyProperty SourceIdProperty =
			DependencyProperty.Register("SourceId", typeof(string), typeof(YCodeLine));

		public string TargetId
		{
			get { return (string)GetValue(TargetIdProperty); }
			set { SetValue(TargetIdProperty, value); }
		}

		public static readonly DependencyProperty TargetIdProperty =
			DependencyProperty.Register("TargetId", typeof(string), typeof(YCodeLine));

		#endregion

		protected override Geometry DefiningGeometry { get; }

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			if (!_isLoaded)
			{
				var canvas = this.FindParent<YCodeCanvas>();

				if (canvas != null)
				{
					_canvas = canvas;

					if (this.Source == null || this.Target == null)
					{
						var nodes = _canvas.Children.OfType<YCodeNode>();

						var sObj = nodes.FirstOrDefault(x => this.SourceId.Equals(x.NodeId));

						var tObj = nodes.FirstOrDefault(x => this.TargetId.Equals(x.NodeId));

						if (sObj != null)
						{
							this.Source = sObj;
						}

						if (tObj != null)
						{
							this.Target = tObj;
						}
					}

					if (this.Source != null && this.Target != null)
					{
						this.Source.Changed += this.OnChanged;

						this.Target.Changed += this.OnChanged;

						this.Source.Lines.Add(this);

						this.Target.Lines.Add(this);
					}

					this.OnChanged(this, EventArgs.Empty);
				}

				_isLoaded = true;
			}
		}

		private void OnChanged(object? sender, EventArgs e)
		{
			var start = this.Source.Left;

			var end = this.Target.Left;

			var p1 = new Point(start.X - 30, start.Y);

			var p2 = new Point(end.X - 30, end.Y);

			if (this.Source.Right.X < this.Target.Left.X)
			{
				start = this.Source.Right;

				p1.X = start.X + 30;
			}
			else if (this.Source.Left.X > this.Target.Right.X)
			{
				end = this.Target.Right;

				p2.X = end.X + 30;
			}

			_figure.StartPoint = start;

			_segment.Point1 = p1;
			_segment.Point2 = p2;
			_segment.Point3 = end;
		}
	}
}
