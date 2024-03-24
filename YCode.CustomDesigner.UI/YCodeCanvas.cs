using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YCode.CustomDesigner.UI.Events;

namespace YCode.CustomDesigner.UI
{
	public class YCodeCanvas : Canvas
	{
		private Point? _elementPoint;

		public YCodeCanvas()
		{
			this.OnLoadMenu();
		}

		internal UIElement? CurrentElement { get; set; }

		public event EventHandler<YCodeNodeDeletedEventArgs>? ElementDeleted;
		public event EventHandler<YCodeNodeDeletingEventArgs>? ElementDeleting;

		#region Dependency Property

		public ICommand MenuDeletedCommand
		{
			get { return (ICommand)GetValue(MenuDeletedCommandProperty); }
			set { SetValue(MenuDeletedCommandProperty, value); }
		}

		public static readonly DependencyProperty MenuDeletedCommandProperty =
			DependencyProperty.Register("MenuDeletedCommand", typeof(ICommand), typeof(YCodeCanvas));

		#endregion

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

		internal void OnNodeDeleted(YCodeNode removeItem)
		{
			if (removeItem != null)
			{
				var deleting = new YCodeNodeDeletingEventArgs(removeItem);

				this.ElementDeleting?.Invoke(this, deleting);

				if (!deleting.Cancel)
				{
					this.Children.Remove(removeItem);

					this.ElementDeleted?.Invoke(this, new YCodeNodeDeletedEventArgs(removeItem));
				}
			}
		}

		internal void OnLoadMenu()
		{
			this.ContextMenu = new ContextMenu();

			var menuItem1 = new MenuItem
			{
				Header = "选项1"
			};
			this.ContextMenu.Items.Add(menuItem1);

			var menuItem2 = new MenuItem
			{
				Header = "选项2"
			};
			this.ContextMenu.Items.Add(menuItem2);
		}
	}
}
