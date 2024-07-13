using System.Windows;
using System.Windows.Controls;

namespace YCode.CustomDesigner.UI
{
	public class YCodeColumnItem : ContentControl
	{
		static YCodeColumnItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(YCodeColumnItem), new FrameworkPropertyMetadata(typeof(YCodeColumnItem)));
		}

		#region Dependency Property

		public string ColumnId
		{
			get { return (string)GetValue(ColumnIdProperty); }
			set { SetValue(ColumnIdProperty, value); }
		}

		public static readonly DependencyProperty ColumnIdProperty =
			DependencyProperty.Register("ColumnId", typeof(string), typeof(YCodeColumnItem), new PropertyMetadata(String.Empty));

		#endregion
	}
}
