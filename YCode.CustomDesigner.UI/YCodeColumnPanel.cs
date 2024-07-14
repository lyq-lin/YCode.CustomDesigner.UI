namespace YCode.CustomDesigner.UI
{
	public class YCodeColumnPanel : ItemsControl
	{
		static YCodeColumnPanel()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(YCodeColumnPanel),
				new FrameworkPropertyMetadata(typeof(YCodeColumnPanel)));
		}

		private ScrollViewer? _scroll;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (this.GetTemplateChild("PART_Scroll") is ScrollViewer scroll)
			{
				_scroll = scroll;
			}
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is YCodeColumnItem;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new YCodeColumnItem();
		}
	}
}
