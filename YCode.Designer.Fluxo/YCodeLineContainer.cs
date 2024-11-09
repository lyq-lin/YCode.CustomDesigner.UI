namespace YCode.Designer.Fluxo;

public class YCodeLineContainer : Selector
{
    static YCodeLineContainer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeLineContainer),
            new FrameworkPropertyMetadata(typeof(YCodeLineContainer))
        );

        FocusableProperty.OverrideMetadata(typeof(YCodeLineContainer), new FrameworkPropertyMetadata(false));
    }

    private YCodeDesigner? _designer;

    public YCodeLineContainer()
    {
        
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (this.FindParent<YCodeDesigner>() is { } designer)
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