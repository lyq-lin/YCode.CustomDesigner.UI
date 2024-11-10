namespace YCode.Designer.Fluxo;

internal class FluxoPendingLine : Adorner
{
    private readonly FluxoDesigner _designer;
    private readonly FluxoLineFactory _lineFactory;
    private readonly StreamGeometry _geometry;
    private readonly Pen _pen;
    private Point? _mouse;

    private IFluxoLineGeometry? _type;

    public FluxoPendingLine(FluxoDesigner designer, FluxoNode node, string currentItem) : base(designer.ItemsHost)
    {
        _designer = designer;

        _lineFactory = new FluxoLineFactory(_designer);

        _pen = new Pen(_designer.LineBrush, 2)
        {
            LineJoin = PenLineJoin.Round,
            DashStyle = new DashStyle([10, 5], 0)
        };

        _geometry = new StreamGeometry()
        {
            FillRule = FillRule.EvenOdd
        };

        this.SourceItem = currentItem;

        this.Source = node;
    }

    protected string SourceItem { get; }
    protected FluxoNode Source { get; }

    protected FluxoNode? Target { get; set; }
    protected string TargetItem { get; set; } = String.Empty;

    protected UIElement? PrevNode { get; set; }
    protected UIElement? PrevConnector { get; set; }
    protected UIElement? PrevItem { get; set; }

    protected Geometry DefiningGeometry
    {
        get
        {
            using (var context = _geometry.Open())
            {
                if (_mouse.HasValue)
                {
                    this.DrawLine(context, _mouse.Value);
                }
            }

            return _geometry;
        }
    }

    #region Attach Properties

    public static readonly DependencyProperty IsOverElementProperty = DependencyProperty.RegisterAttached(
        "IsOverElement", typeof(bool), typeof(FluxoPendingLine), new PropertyMetadata(false));

    public static void SetIsOverElement(DependencyObject element, bool value)
    {
        element.SetValue(IsOverElementProperty, value);
    }

    public static bool GetIsOverElement(DependencyObject element)
    {
        return (bool)element.GetValue(IsOverElementProperty);
    }

    #endregion

    private void DrawLine(StreamGeometryContext context, Point mouse)
    {
        _type ??= _lineFactory.GetLine(_designer.LineType);

        var parameter =
            new FluxoLineParameter((this.Source.Left, this.Source.Right, this.Source.Top, this.Source.Bottom),
                (mouse, mouse, mouse, mouse));

        _type?.DrawLine(parameter, context);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        drawingContext.DrawGeometry(null, _pen, this.DefiningGeometry);

        drawingContext.DrawRectangle(Brushes.Transparent, null, _designer.ItemsHost.Extent);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if (!this.IsMouseCaptured)
            {
                this.CaptureMouseSafe();
            }

            _mouse = e.GetPosition(this);

            if (this.Cursor != Cursors.Cross)
            {
                this.Cursor = Cursors.Cross;
            }

            this.InvalidateVisual();

            this.OnResetIsOverElement();
        }
        else
        {
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }
        }
    }

    protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
    {
        if (this.IsMouseCaptured)
        {
            this.ReleaseMouseCapture();
        }

        if (_designer.ItemsHost.InputHitTest(e.GetPosition(this)) is UIElement element)
        {
            var node = element.FindParent<FluxoNode>();

            var item = element.FindParent<FluxoDataPanelItem>();

            if (item != null)
            {
                this.TargetItem = item.ItemId;
            }

            if (node != null)
            {
                this.Target = node;
            }

            if (this.Target != null)
            {
                this.InvalidateVisual();
            }
        }

        this.CreateLine();

        var layer = AdornerLayer.GetAdornerLayer(_designer.ItemsHost);

        layer?.Remove(this);
    }

    private void CreateLine()
    {
        if (this.Target == null) return;

        var lines = (IList)_designer.Lines;

        var line = new FluxoLineViewModel()
        {
            PrevId = this.Source.NodeId,
            NextId = this.Target.NodeId,
        };

        if (!lines.Contains(line))
        {
            lines.Add(line);
        }
    }

    private void OnResetIsOverElement()
    {
        var connector = _designer.GetPotentialElement<FluxoConnector>();

        if (this.PrevConnector != null)
        {
            SetIsOverElement(this.PrevConnector, false);
        }

        SetIsOverElement(connector, true);

        this.PrevConnector = connector;

        var item = _designer.GetPotentialElement<FluxoDataPanelItem>();

        if (this.PrevItem != null)
        {
            SetIsOverElement(this.PrevItem, false);
        }

        SetIsOverElement(item, true);

        this.PrevItem = item;

        var node = _designer.GetPotentialElement<FluxoNode>();

        if (this.PrevNode != null)
        {
            SetIsOverElement(this.PrevNode, false);
        }

        SetIsOverElement(node, true);

        this.PrevNode = node;
    }
}