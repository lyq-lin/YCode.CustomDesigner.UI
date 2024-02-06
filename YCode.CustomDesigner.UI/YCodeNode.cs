using System.Windows;
using System.Windows.Controls;

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

		public YCodeNode()
		{
			this.Lines = new();

			LayoutUpdated += this.OnLayoutUpdated;
		}

		internal Point Left { get; private set; }
		internal Point Right { get; private set; }

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
	}
}
