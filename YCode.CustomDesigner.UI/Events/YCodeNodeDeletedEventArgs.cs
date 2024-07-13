using System.Windows;

namespace YCode.CustomDesigner.UI
{
	public class YCodeNodeDeletedEventArgs : EventArgs
	{
		public YCodeNodeDeletedEventArgs(UIElement element)
		{
			this.RemoveItem = element;
		}

		public UIElement RemoveItem { get; set; }
	}
}
