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

		internal Point Left { get; private set; }
		internal Point Right { get; private set; }
	}
}
