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

    private FluxoNode? _node;

    internal FluxoNode Node => _node ??
                               throw new InvalidOperationException(
                                   $"FluxoNode is missing or is not of type {nameof(FluxoNode)}.");


    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (this.FindParent<FluxoNode>() is { } node)
        {
            _node = node;
        }
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