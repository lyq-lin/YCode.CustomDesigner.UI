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

        this.Lines = [];

        this.LayoutUpdated += OnLayoutUpdateChanged;
    }

    internal event EventHandler? Changed;

    internal Point Left { get; private set; }
    internal Point Right { get; private set; }
    internal Point Top { get; private set; }
    internal Point Bottom { get; private set; }

    internal List<YCodeLine> Lines { get; private set; }

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

    private void OnLayoutUpdateChanged(object? sender, EventArgs e)
    {
        var isChanged = false;

        var left = this.Location.X;
        var right = this.Location.X + this.ActualWidth;
        var top = this.Location.Y;
        var bottom = this.Location.Y + this.ActualHeight;

        var x = left + (this.ActualWidth / 2);
        var y = top + (this.ActualHeight / 2);

        var newLeft = new Point(left, y);
        var newRight = new Point(right, y);
        var newTop = new Point(x, top);
        var newBottom = new Point(x, bottom);

        if (this.Left != newLeft)
        {
            this.Left = newLeft;

            isChanged = true;
        }

        if (this.Right != newRight)
        {
            this.Right = newRight;

            isChanged = true;
        }

        if (this.Top != newTop)
        {
            this.Top = newTop;

            isChanged = true;
        }

        if (this.Bottom != newBottom)
        {
            this.Bottom = newBottom;

            isChanged = true;
        }

        if (isChanged)
        {
            this.Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}