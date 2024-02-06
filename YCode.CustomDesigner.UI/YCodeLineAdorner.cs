using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace YCode.CustomDesigner.UI
{
	internal class YCodeLineAdorner : Adorner
	{
		private readonly YCodeCanvas _canvas;

		private readonly PathGeometry _geometry;
		private readonly PathFigure _figure;
		private readonly BezierSegment _segment;
		private readonly Pen _pen;

		public YCodeLineAdorner(YCodeCanvas canvas, YCodeNode host) : base(canvas)
		{
			_canvas = canvas;

			_figure = new PathFigure();

			_segment = new BezierSegment();

			_figure.Segments.Add(_segment);

			_geometry = new PathGeometry(new PathFigure[] { _figure });

			_pen = new Pen(Brushes.Purple, 2) { LineJoin = PenLineJoin.Round };

			this.HostNode = host;
		}

		internal YCodeNode HostNode { get; set; }
		internal YCodeNode? AttachNode { get; set; }

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			drawingContext.DrawGeometry(null, _pen, _geometry);

			drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Pressed)
			{
				this.OnChanged(e.GetPosition(this));

				this.InvalidateVisual();
			}
			else
			{
				if (this.IsMouseCaptured)
				{
					this.ReleaseMouseCapture();
				}
			}
		}

		protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
		{
			if (this.IsMouseCaptured)
			{
				this.ReleaseMouseCapture();
			}

			if (_canvas.InputHitTest(e.GetPosition(this)) is UIElement element)
			{
				var node = element.FindParent<YCodeNode>();

				if (node != null)
				{
					this.AttachNode = node;

					this.OnChanged(e.GetPosition(this));

					this.InvalidateVisual();
				}
			}

			if (this.AttachNode != null)
			{
				//TO DO: 适配MVVM模式

				var line = new YCodeLine()
				{
					Source = this.HostNode,
					SourceId = this.HostNode.Name,
					Target = this.AttachNode,
					TargetId = this.AttachNode.Name
				};

				_canvas.Children.Add(line);
			}

			var adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);

			if (adornerLayer != null)
			{
				adornerLayer.Remove(this);
			}
		}

		private void OnChanged(Point mouse)
		{
			var start = this.HostNode.Left;

			var end = mouse;

			var p1 = new Point(start.X - 30, start.Y);

			var p2 = new Point(end.X - 30, end.Y);

			if (this.HostNode.Right.X < mouse.X)
			{
				start = this.HostNode.Right;

				p1.X = start.X + 30;
			}
			else if (this.HostNode.Left.X > mouse.X)
			{
				end = mouse;

				p2.X = end.X + 30;
			}

			_figure.StartPoint = start;

			_segment.Point1 = p1;
			_segment.Point2 = p2;
			_segment.Point3 = end;
		}
	}
}
