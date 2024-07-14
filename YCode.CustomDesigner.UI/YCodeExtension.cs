using System.Windows.Media;

namespace YCode.CustomDesigner.UI
{
	internal static class YCodeExtension
	{
		internal static T? FindParent<T>(this UIElement element) where T : UIElement
		{
			if (element is T value)
			{
				return value;
			}
			else
			{
				var parent = VisualTreeHelper.GetParent(element);

				if (parent is UIElement last)
				{
					return last.FindParent<T>();
				}
			}

			return default;
		}
	}
}
