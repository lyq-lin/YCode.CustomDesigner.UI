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

			CommandManager.RegisterClassCommandBinding(
				typeof(YCodeNode),
				new CommandBinding(DeletedCommand, OnNodeDeleted));
		}

		public YCodeNode()
		{
			this.Lines = new();

			LayoutUpdated += this.OnLayoutUpdated;

			Console.WriteLine("Test1");

			Loaded += this.OnLoaded;
		}

		internal YCodeCanvas? YCodeCanvas { get; set; }

		internal Point Left { get; private set; }
		internal Point Right { get; private set; }

		internal Point? DragStartPoint { get; set; } = null;

		internal List<YCodeLine> Lines { get; set; }

		internal event EventHandler? Changed;

		public static ICommand DeletedCommand = new RoutedCommand();

		#region Dependency Property

		public string NodeId
		{
			get { return (string)GetValue(NodeIdProperty); }
			set { SetValue(NodeIdProperty, value); }
		}

		public static readonly DependencyProperty NodeIdProperty =
			DependencyProperty.Register("NodeId", typeof(string), typeof(YCodeNode), new PropertyMetadata(String.Empty));

		#endregion

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			var canvas = this.FindParent<YCodeCanvas>();

			if (canvas != null)
			{
				YCodeCanvas = canvas;
			}
		}

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
			if (YCodeCanvas != null)
			{
				this.DragStartPoint = new Point?(e.GetPosition(YCodeCanvas));

				e.Handled = true;
			}
		}

		protected override void OnPreviewMouseMove(MouseEventArgs e)
		{
			if (YCodeCanvas != null)
			{
				if (e.RightButton != MouseButtonState.Pressed)
				{
					this.DragStartPoint = null;
				}

				if (this.DragStartPoint.HasValue)
				{
					//创建装饰器
					var adornerLayer = AdornerLayer.GetAdornerLayer(YCodeCanvas);

					if (adornerLayer != null)
					{
						var lineAdorner = new YCodeLineAdorner(YCodeCanvas, this);

						if (lineAdorner != null)
						{
							adornerLayer.Add(lineAdorner);

							e.Handled |= true;
						}
					}
				}
			}
		}

		private static void OnNodeDeleted(object sender, ExecutedRoutedEventArgs e)
		{
			if (sender is YCodeNode node)
			{
				var canvas = node.FindParent<YCodeCanvas>();

				canvas?.OnNodeDeleted(node);
			}
		}
	}
}
