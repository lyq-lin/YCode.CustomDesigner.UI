namespace YCode.CustomDesigner.UI;

public class YCodeNode : ContentControl, IDesignerItem
{
    private readonly YCodeDesigner _designer;

    static YCodeNode()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeNode),
            new FrameworkPropertyMetadata(typeof(YCodeNode))
        );
    }

    public YCodeNode(YCodeDesigner designer)
    {
        _designer = designer;
    }

    #region Dependency Properties

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
        nameof(Location), typeof(Point), typeof(YCodeNode), new PropertyMetadata(default(Point)));

    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId), typeof(string), typeof(YCodeNode), new PropertyMetadata(String.Empty));

    public string NodeId
    {
        get => (string)GetValue(NodeIdProperty);
        set => SetValue(NodeIdProperty, value);
    }

    public Point Location
    {
        get => (Point)GetValue(LocationProperty);
        set => SetValue(LocationProperty, value);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }
}