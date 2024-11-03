namespace YCode.CustomDesigner.UI;

public class YCodeDataPanel : ItemsControl
{
    static YCodeDataPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeDataPanel),
            new FrameworkPropertyMetadata(typeof(YCodeDataPanel))
        );
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new YCodeDataPanelItem(this);
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is YCodeDataPanelItem;
    }
}