using System.Windows;

namespace YCode.CustomDesigner.UI
{
	public class YCodeColumnNode : YCodeNode
	{
		static YCodeColumnNode()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(YCodeColumnNode), new FrameworkPropertyMetadata(typeof(YCodeColumnNode)));
		}
	}
}
