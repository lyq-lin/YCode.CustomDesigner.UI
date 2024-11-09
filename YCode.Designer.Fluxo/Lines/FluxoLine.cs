using System.Diagnostics;

namespace YCode.Designer.Fluxo;

public class FluxoLine : Shape
{
    static FluxoLine()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoLine),
            new FrameworkPropertyMetadata(typeof(FluxoLine))
        );
    }

    private bool _isLoaded;
    private readonly FluxoDesigner _designer;
    private readonly FluxoLineContainer _container;
    private readonly StreamGeometry _geometry;
    private readonly FluxoLineFactory _factory;

    public FluxoLine(FluxoDesigner designer, FluxoLineContainer container)
    {
        _designer = designer;

        _container = container;

        _geometry = new StreamGeometry()
        {
            FillRule = FillRule.EvenOdd
        };

        _factory = new FluxoLineFactory(_designer);

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

    internal FluxoNode? Source { get; private set; }
    internal FluxoNode? Target { get; private set; }

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

    private (Point, Point) DrawLine(StreamGeometryContext context, FluxoNode source, FluxoNode target)
    {
        var line = _factory.GetLine(_designer.LineType);

        var @params = new FluxoLineParameter(
            (source.Left, source.Right, source.Top, source.Bottom),
            (target.Left, target.Right, target.Top, target.Bottom));

        line?.DrawLine(@params, context);

        return (@params.Start, @params.End);
    }

    #region Dependency Properties

    public static readonly DependencyProperty SourceIdProperty = DependencyProperty.Register(
        nameof(SourceId), typeof(string), typeof(FluxoLine),
        new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty TargetIdProperty = DependencyProperty.Register(
        nameof(TargetId), typeof(string), typeof(FluxoLine),
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

            if (this.FindNode(sourceId) is FluxoNode node)
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

            if (this.FindNode(targetId) is FluxoNode node)
            {
                this.Target = node;
            }
        }

        base.OnPropertyChanged(e);
    }

    private DependencyObject? FindNode(string nodeId)
    {
        var obj = _designer.ItemsSource.OfType<FluxoNodeViewModel>().FirstOrDefault(x => nodeId.Equals(x.Id));

        var element = _designer.ItemContainerGenerator.ContainerFromItem(obj);

        if (element is FluxoNode node)
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