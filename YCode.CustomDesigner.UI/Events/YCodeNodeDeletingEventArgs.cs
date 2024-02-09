using System.ComponentModel;
using System.Windows;

namespace YCode.CustomDesigner.UI.Events
{
	public class YCodeNodeDeletingEventArgs : CancelEventArgs
	{
		public YCodeNodeDeletingEventArgs(UIElement element)
		{
			this.RemoveItem = element;
		}

		public UIElement RemoveItem { get; set; }
	}
}
