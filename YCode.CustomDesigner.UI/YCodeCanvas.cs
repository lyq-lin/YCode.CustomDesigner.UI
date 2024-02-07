using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YCode.CustomDesigner.UI
{
	public class YCodeCanvas : Canvas
	{
		private Point? _elementPoint;

		internal UIElement? CurrentElement { get; set; }

		protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			if (e.Source is UIElement element)
			{
				var node = element.FindParent<YCodeNode>();

				if (node != null)
				{
					this.CurrentElement = node;

					_elementPoint = e.GetPosition(node);

					return;
				}
			}

			this.CurrentElement = null;

			_elementPoint = null;
		}

		protected override void OnPreviewMouseMove(MouseEventArgs e)
		{
			var mouse = e.GetPosition(this);

			if (this.CurrentElement != null
				&& _elementPoint != null
				&& e.LeftButton == MouseButtonState.Pressed)
			{
				var left = mouse.X - _elementPoint.Value.X;

				var top = mouse.Y - _elementPoint.Value.Y;

				YCodeCanvas.SetLeft(this.CurrentElement, left);

				YCodeCanvas.SetTop(this.CurrentElement, top);
			}
		}
	}
}
