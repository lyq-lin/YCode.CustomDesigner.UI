using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YCode.CustomDesigner.UI
{
	public class YCodeCanvas : Canvas
	{
		private Point? _elementPoint;
		private YCodeItemSourceManager _itemSourceManager;

		public YCodeCanvas()
		{
			_itemSourceManager = new YCodeItemSourceManager(this);
		}

		internal UIElement? CurrentElement { get; set; }

		#region Dependency Property

		public bool IsSort
		{
			get { return (bool)GetValue(IsSortProperty); }
			set { SetValue(IsSortProperty, value); }
		}

		public YCodeSourceViewModel Source
		{
			get { return (YCodeSourceViewModel)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(YCodeSourceViewModel), typeof(YCodeCanvas));

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
					this.IsSort = false;
				}
			}
			else if (SourceProperty == e.Property && e.NewValue is not null)
			{
				_itemSourceManager.OnChanged();
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
