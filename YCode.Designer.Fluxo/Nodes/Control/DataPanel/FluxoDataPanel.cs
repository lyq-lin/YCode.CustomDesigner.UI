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
    private ScrollViewer? _scroll;

    internal event EventHandler? Changed;

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

        if (this.GetTemplateChild("PART_Scroll") is ScrollViewer scroll)
        {
            _scroll = scroll;

            _scroll.ScrollChanged += OnScrollChanged;
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

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        this.Changed?.Invoke(this.Node, e);

        this.Node.InvalidateNode();
    }
}