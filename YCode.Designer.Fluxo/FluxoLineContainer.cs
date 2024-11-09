namespace YCode.Designer.Fluxo;

public class FluxoLineContainer : Selector
{
    static FluxoLineContainer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoLineContainer),
            new FrameworkPropertyMetadata(typeof(FluxoLineContainer))
        );

        FocusableProperty.OverrideMetadata(typeof(FluxoLineContainer), new FrameworkPropertyMetadata(false));
    }

    private FluxoDesigner? _designer;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (this.FindParent<FluxoDesigner>() is { } designer)
        {
            _designer = designer;
        }
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return _designer?.GetContainerForLineOverride(this) ?? base.GetContainerForItemOverride();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return _designer?.IsLineItsOwnContainerOverride(this,item) ?? base.IsItemItsOwnContainerOverride(item);
    }
}