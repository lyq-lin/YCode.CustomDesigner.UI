namespace YCode.CustomDesigner.UI;

public class YCodeLine : Shape
{
    static YCodeLine()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeLine),
            new FrameworkPropertyMetadata(typeof(YCodeLine))
        );
    }

    private bool _isLoaded;
    private readonly YCodeDesigner _designer;
    private readonly YCodeLineContainer _container;
    private readonly StreamGeometry _geometry;
    private readonly YCodeLineFactory _factory;

    public YCodeLine(YCodeDesigner designer, YCodeLineContainer container)
    {
        _designer = designer;

        _container = container;

        _geometry = new StreamGeometry()
        {
            FillRule = FillRule.EvenOdd
        };

        _factory = new YCodeLineFactory(_designer);

        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!_isLoaded)
        {
            this.OnChanged(null, EventArgs.Empty);

            _isLoaded = true;
        }
    }

    internal event EventHandler? Changed;

    internal YCodeNode? Source { get; private set; }
    internal YCodeNode? Target { get; private set; }

    protected override Geometry DefiningGeometry
    {
        get
        {
            using (var context = _geometry.Open())
            {
                if (this.Source != null && this.Target != null)
                {
                    (Point start, Point end) = this.DrawLine(context, this.Source, this.Target);

                    this.Changed?.Invoke(this, EventArgs.Empty);
                }
            }

            return _geometry;
        }
    }

    private (Point, Point) DrawLine(StreamGeometryContext context, YCodeNode source, YCodeNode target)
    {
        var line = _factory.GetLine(_designer.LineType);

        var @params = new YCodeLineParameter(
            (source.Left, source.Right, source.Top, source.Bottom),
            (target.Left, target.Right, target.Top, target.Bottom));

        line?.DrawLine(@params, context);

        return (@params.Start, @params.End);
    }

    #region Dependency Properties

    public static readonly DependencyProperty SourceIdProperty = DependencyProperty.Register(
        nameof(SourceId), typeof(string), typeof(YCodeLine),
        new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty TargetIdProperty = DependencyProperty.Register(
        nameof(TargetId), typeof(string), typeof(YCodeLine),
        new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string TargetId
    {
        get => (string)GetValue(TargetIdProperty);
        set => SetValue(TargetIdProperty, value);
    }

    public string SourceId
    {
        get => (string)GetValue(SourceIdProperty);
        set => SetValue(SourceIdProperty, value);
    }

    #endregion

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property == SourceIdProperty && e.NewValue is string sourceId)
        {
            if (this.Source != null && this.Source.NodeId != sourceId)
            {
                this.Source.Lines.Remove(this);

                this.Source = null;
            }

            if (this.FindNode(sourceId) is YCodeNode node)
            {
                this.Source = node;
            }
        }
        else if (e.Property == TargetIdProperty && e.NewValue is string targetId)
        {
            if (this.Target != null && this.Target.NodeId != targetId)
            {
                this.Target.Lines.Remove(this);

                this.Target = null;
            }

            if (this.FindNode(targetId) is YCodeNode node)
            {
                this.Target = node;
            }
        }

        base.OnPropertyChanged(e);
    }

    private DependencyObject? FindNode(string nodeId)
    {
        var obj = _designer.ItemsSource.OfType<YCodeNodeViewModel>().FirstOrDefault(x => nodeId.Equals(x.Id));

        var element = _designer.ItemContainerGenerator.ContainerFromItem(obj);

        if (element is YCodeNode node)
        {
            node.Changed -= OnChanged;

            node.Changed += OnChanged;

            if (!node.Lines.Contains(this))
            {
                node.Lines.Add(this);
            }

            return node;
        }

        return element;
    }

    private void OnChanged(object? sender, EventArgs e)
    {
        this.InvalidateVisual();
    }
}