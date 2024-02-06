using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace YCode.CustomDesigner.UI
{
	public class YCodeNode : ContentControl
	{
		static YCodeNode()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(YCodeNode),
				new FrameworkPropertyMetadata(typeof(YCodeNode)));
		}

		private YCodeCanvas? _canvas;

		public YCodeNode()
		{
			this.Lines = new();

			LayoutUpdated += this.OnLayoutUpdated;

			Loaded += this.OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			var canvas = this.FindParent<YCodeCanvas>();

			if (canvas != null)
			{
				_canvas = canvas;
			}
		}

		internal Point Left { get; private set; }
		internal Point Right { get; private set; }

		internal Point? DragStartPoint { get; set; } = null;

		internal List<YCodeLine> Lines { get; set; }

		internal event EventHandler? Changed;

		private void OnLayoutUpdated(object? sender, EventArgs e)
		{
			var isMove = false;

			var left = Double.IsNaN(YCodeCanvas.GetLeft(this)) ? 0 : YCodeCanvas.GetLeft(this);
			var right = left + this.ActualWidth;
			var top = Double.IsNaN(YCodeCanvas.GetTop(this)) ? 0 : YCodeCanvas.GetTop(this);
			var bottom = top + this.ActualHeight;

			var x = left + (this.ActualWidth / 2);

			var y = top + (this.ActualHeight / 2);

			var leftPoint = new Point(left, y);

			var rightPoint = new Point(right, y);

			if (this.Left != leftPoint)
			{
				this.Left = leftPoint;

				isMove = true;
			}

			if (this.Right != rightPoint)
			{
				this.Right = rightPoint;

				isMove = true;
			}

			if (isMove)
			{
				this.Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
		{
			if (_canvas != null)
			{
				this.DragStartPoint = new Point?(e.GetPosition(_canvas));

				e.Handled = true;
			}
		}

		protected override void OnPreviewMouseMove(MouseEventArgs e)
		{
			if (_canvas != null)
			{
				if (e.RightButton != MouseButtonState.Pressed)
				{
					this.DragStartPoint = null;
				}

				if (this.DragStartPoint.HasValue)
				{
					//创建装饰器
					var adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);

					if (adornerLayer != null)
					{
						var lineAdorner = new YCodeLineAdorner(_canvas, this);

						if (lineAdorner != null)
						{
							adornerLayer.Add(lineAdorner);

							e.Handled |= true;
						}
					}
				}
			}
		}
	}
}
