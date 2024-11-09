namespace YCode.Designer.Fluxo;

public class FluxoDataPanel : ItemsControl
{
    static FluxoDataPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoDataPanel),
            new FrameworkPropertyMetadata(typeof(FluxoDataPanel))
        );
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new FluxoDataPanelItem(this);
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is FluxoDataPanelItem;
    }
}