using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YCode.CustomDesigner.UI.Events;

namespace YCode.CustomDesigner.UI
{
	public class YCodeCanvas : Canvas
	{
		private Point? _elementPoint;
		private YCodeSortManager _sortManager;


		public YCodeCanvas()
		{
			_sortManager = new YCodeSortManager(this);
		}

		internal UIElement? CurrentElement { get; set; }

		#region Dependency Property

		public bool IsSort
		{
			get { return (bool)GetValue(IsSortProperty); }
			set { SetValue(IsSortProperty, value); }
		}

		public static readonly DependencyProperty IsSortProperty =
			DependencyProperty.Register("IsSort", typeof(bool), typeof(YCodeCanvas), new PropertyMetadata(false));

		#endregion

		public event EventHandler<YCodeNodeDeletedEventArgs>? ElementDeleted;
		public event EventHandler<YCodeNodeDeletingEventArgs>? ElementDeleting;

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);

			if (IsSortProperty == e.Property && e.NewValue is bool flag)
			{
				if (flag)
				{
					_sortManager.Layout(1);

					this.IsSort = false;
				}
			}
		}

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


	}
}
