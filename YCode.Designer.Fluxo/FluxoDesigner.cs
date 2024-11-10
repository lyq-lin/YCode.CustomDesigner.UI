using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace YCode.Designer.Fluxo;

public partial class FluxoDesigner : MultiSelector
{
    static FluxoDesigner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoDesigner),
            new FrameworkPropertyMetadata(typeof(FluxoDesigner))
        );

        FocusableProperty.OverrideMetadata(typeof(FluxoDesigner), new FrameworkPropertyMetadata(false));
    }

    private bool? _isLoaded;
    private DispatcherTimer? _autoPanningTimer;

    private readonly TranslateTransform _translateTransform = new();
    private readonly ScaleTransform _scaleTransform = new();

    public FluxoDesigner()
    {
        this.AddHandler(FluxoNode.DragStartedEvent, new DragStartedEventHandler(this.OnNodeDragStarted));
        this.AddHandler(FluxoNode.DragDeltaEvent, new DragDeltaEventHandler(this.OnNodeDragDelta));
        this.AddHandler(FluxoNode.DragCompletedEvent, new DragCompletedEventHandler(this.OnNodeDragCompleted));

        var transform = new TransformGroup();
        transform.Children.Add(_scaleTransform);
        transform.Children.Add(_translateTransform);

        this.SetValue(ViewportTransformPropertyKey, transform);

        this.Loaded += OnLoaded;
    }

    internal FluxoPanel ItemsHost { get; private set; } = default!;

    internal bool IsPanning { get; private set; }

    internal Point? Point { get; private set; }

    #region Dependency Properties

    protected internal static readonly DependencyPropertyKey ViewportTransformPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(ViewportTransform),
            typeof(Transform),
            typeof(FluxoDesigner),
            new FrameworkPropertyMetadata(new TransformGroup())
        );

    public static readonly DependencyProperty ViewportTransformProperty =
        ViewportTransformPropertyKey.DependencyProperty;

    public static readonly DependencyProperty ItemsExtentProperty = DependencyProperty.Register(
        nameof(ItemsExtent), typeof(Rect), typeof(FluxoDesigner), new PropertyMetadata(default(Rect)));

    public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
        nameof(Zoom), typeof(double), typeof(FluxoDesigner), new PropertyMetadata(1d));

    public static readonly DependencyProperty MaxZoomProperty = DependencyProperty.Register(
        nameof(MaxZoom), typeof(double), typeof(FluxoDesigner), new PropertyMetadata(2d));

    public static readonly DependencyProperty MinZoomProperty = DependencyProperty.Register(
        nameof(MinZoom), typeof(double), typeof(FluxoDesigner), new PropertyMetadata(0.5d));

    public static readonly DependencyProperty ViewportLocationProperty = DependencyProperty.Register(
        nameof(ViewportLocation), typeof(Point), typeof(FluxoDesigner),
        new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnViewportLocationChanged));

    public static readonly DependencyProperty ViewportSizeProperty = DependencyProperty.Register(
        nameof(ViewportSize), typeof(Size), typeof(FluxoDesigner), new PropertyMetadata(default(Size)));

    public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
        nameof(SelectedItems), typeof(IList), typeof(FluxoDesigner), new PropertyMetadata(default(IList)));

    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        nameof(Source), typeof(FluxoSource), typeof(FluxoDesigner));

    public static readonly DependencyProperty LinesProperty = DependencyProperty.Register(
        nameof(Lines), typeof(IEnumerable), typeof(FluxoDesigner));

    public static readonly DependencyProperty ItemAdapterProperty = DependencyProperty.Register(
        nameof(ItemAdapter), typeof(IFluxoAdapter), typeof(FluxoDesigner));

    public static readonly DependencyProperty LineTypeProperty = DependencyProperty.Register(
        nameof(LineType), typeof(LineType), typeof(FluxoDesigner), new PropertyMetadata(LineType.Bezier));

    public static readonly DependencyProperty CanAutoPanningProperty = DependencyProperty.Register(
        nameof(CanAutoPanning), typeof(bool), typeof(FluxoDesigner),
        new PropertyMetadata(true, OnCanAutoPanningChanged));

    public static readonly DependencyProperty GridTypeProperty = DependencyProperty.Register(
        nameof(GridType), typeof(GridType), typeof(FluxoDesigner),
        new FrameworkPropertyMetadata(GridType.Grid, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty ToolProperty = DependencyProperty.Register(
        nameof(Tool), typeof(object), typeof(FluxoDesigner));

    public static readonly DependencyProperty ToolTemplateProperty = DependencyProperty.Register(
        nameof(ToolTemplate), typeof(DataTemplate), typeof(FluxoDesigner));

    public static readonly DependencyProperty HorizontalToolAlignmentProperty = DependencyProperty.Register(
        nameof(HorizontalToolAlignment), typeof(HorizontalAlignment), typeof(FluxoDesigner),
        new PropertyMetadata(HorizontalAlignment.Center));

    public static readonly DependencyProperty VerticalToolAlignmentProperty = DependencyProperty.Register(
        nameof(VerticalToolAlignment), typeof(VerticalAlignment), typeof(FluxoDesigner),
        new PropertyMetadata(VerticalAlignment.Bottom));

    public static readonly DependencyProperty ToolPaddingProperty = DependencyProperty.Register(
        nameof(ToolPadding), typeof(Thickness), typeof(FluxoDesigner),
        new PropertyMetadata(new Thickness(0, 0, 0, 50)));

    public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register(
        nameof(LineBrush), typeof(Brush), typeof(FluxoDesigner), new PropertyMetadata(Brushes.DodgerBlue));

    public Brush LineBrush
    {
        get => (Brush)GetValue(LineBrushProperty);
        set => SetValue(LineBrushProperty, value);
    }

    public Thickness ToolPadding
    {
        get => (Thickness)GetValue(ToolPaddingProperty);
        set => SetValue(ToolPaddingProperty, value);
    }

    public VerticalAlignment VerticalToolAlignment
    {
        get => (VerticalAlignment)GetValue(VerticalToolAlignmentProperty);
        set => SetValue(VerticalToolAlignmentProperty, value);
    }

    public HorizontalAlignment HorizontalToolAlignment
    {
        get => (HorizontalAlignment)GetValue(HorizontalToolAlignmentProperty);
        set => SetValue(HorizontalToolAlignmentProperty, value);
    }

    public DataTemplate ToolTemplate
    {
        get => (DataTemplate)GetValue(ToolTemplateProperty);
        set => SetValue(ToolTemplateProperty, value);
    }

    public object Tool
    {
        get => (object)GetValue(ToolProperty);
        set => SetValue(ToolProperty, value);
    }

    public GridType GridType
    {
        get => (GridType)GetValue(GridTypeProperty);
        set => SetValue(GridTypeProperty, value);
    }

    public bool CanAutoPanning
    {
        get => (bool)GetValue(CanAutoPanningProperty);
        set => SetValue(CanAutoPanningProperty, value);
    }

    public LineType LineType
    {
        get => (LineType)GetValue(LineTypeProperty);
        set => SetValue(LineTypeProperty, value);
    }

    public IFluxoAdapter? ItemAdapter
    {
        get => (IFluxoAdapter?)GetValue(ItemAdapterProperty);
        set => SetValue(ItemAdapterProperty, value);
    }

    public IEnumerable Lines
    {
        get => (IEnumerable)GetValue(LinesProperty);
        set => SetValue(LinesProperty, value);
    }

    public FluxoSource? Source
    {
        get => (FluxoSource?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public new IList? SelectedItems
    {
        get => (IList?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public Size ViewportSize
    {
        get => (Size)GetValue(ViewportSizeProperty);
        set => SetValue(ViewportSizeProperty, value);
    }

    public Point ViewportLocation
    {
        get => (Point)GetValue(ViewportLocationProperty);
        set => SetValue(ViewportLocationProperty, value);
    }

    public double MinZoom
    {
        get => (double)GetValue(MinZoomProperty);
        set => SetValue(MinZoomProperty, value);
    }

    public double MaxZoom
    {
        get => (double)GetValue(MaxZoomProperty);
        set => SetValue(MaxZoomProperty, value);
    }

    public double Zoom
    {
        get => (double)GetValue(ZoomProperty);
        set => SetValue(ZoomProperty, value);
    }

    public Rect ItemsExtent
    {
        get => (Rect)GetValue(ItemsExtentProperty);
        set => SetValue(ItemsExtentProperty, value);
    }

    public Transform ViewportTransform => (Transform)GetValue(ViewportTransformProperty);

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.ItemsHost = this.GetTemplateChild("PART_ItemsHost") as FluxoPanel ??
                         throw new InvalidOperationException(
                             $"PART_ItemsHost is missing or is not of type {nameof(FluxoPanel)}.");

        this.OnAutoPanningChanged(this.CanAutoPanning);

        this.ApplyRenderingOptimizations();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property == SourceProperty)
        {
            if (e.NewValue is FluxoSource source)
            {
                this.ItemsSource = source.Nodes;

                this.Lines = source.Lines;

                return;
            }
        }

        base.OnPropertyChanged(e);
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new FluxoNode(this)
        {
            RenderTransform = new TranslateTransform()
        };
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is FluxoNode;
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        switch (this.GridType)
        {
            case GridType.Dot:
            {
                this.OnRenderDot(dc);
            }
                break;
            case GridType.Grid:
            {
                this.OnRenderGrid(dc);
            }
                break;
        }
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (Mouse.Captured == null || this.IsMouseCaptured)
        {
            this.Focus();

            this.CaptureMouseSafe();

            this.IsPanning = true;

            this.Cursor = Cursors.SizeAll;

            this.Point = Mouse.GetPosition(this);

            e.Handled = true;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        var mouse = e.GetPosition(this);

        if (this.IsPanning && this.IsMouseCaptureWithin)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.IsPanning = false;

                this.Cursor = Cursors.Arrow;

                this.Point = null;

                if (this.IsMouseCaptured)
                {
                    this.ReleaseMouseCapture();
                }

                return;
            }

            if (this.Point.HasValue)
            {
                var value = mouse - this.Point.Value;

                this.ViewportLocation -= value / this.Zoom;

                this.Point = mouse;
            }
        }
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (this.IsMouseCaptured)
        {
            this.ReleaseMouseCapture();
        }

        if (this.IsPanning)
        {
            this.IsPanning = false;

            this.Cursor = Cursors.Arrow;

            this.Point = null;
        }
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!_isLoaded.HasValue || !_isLoaded.Value)
        {
            _isLoaded = false;

            var mounting = new FluxoMountingEventArgs();

            this.RaiseEvent<FluxoMountingEventArgs>(nameof(Mounting), mounting);

            if (!mounting.Cancel)
            {
                if (this.ItemAdapter != null)
                {
                    var source = await this.ItemAdapter.ImportAsync(mounting.Value);

                    this.Source ??= new FluxoSource();

                    this.Source.Nodes.Clear();

                    this.Source.Lines.Clear();

                    await this.CopyToAsync(source.Nodes, this.Source.Nodes);

                    await this.CopyToAsync(source.Lines, this.Source.Lines);
                }

                this.RaiseEvent<FluxoMountedEventArgs>(nameof(Mounted), new FluxoMountedEventArgs(this.Source));
            }

            _isLoaded = true;
        }
    }

    private void ApplyRenderingOptimizations()
    {
        this.ItemsHost.CacheMode = new BitmapCache(1.0 / 1.0);
    }

    private void OnAutoPanningChanged(bool canAutoPanning)
    {
        if (!canAutoPanning)
        {
            _autoPanningTimer?.Stop();
        }
        else if (_autoPanningTimer == null)
        {
            _autoPanningTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(1d), DispatcherPriority.Background,
                HandleAutoPanning, Dispatcher);
        }
        else
        {
            _autoPanningTimer.Interval = TimeSpan.FromMilliseconds(1d);

            _autoPanningTimer.Start();
        }

        return;

        void HandleAutoPanning(object? sender, EventArgs e)
        {
            if (!IsPanning && IsMouseCaptureWithin)
            {
                var mouse = Mouse.GetPosition(this);

                var rate = 1d;

                var edgeDistance = 15d;

                var autoPanSpeed = Math.Min(15d, 15d * rate) / (this.Zoom * 2);

                var x = this.ViewportLocation.X;

                var y = this.ViewportLocation.Y;

                if (mouse.X <= edgeDistance)
                {
                    x -= autoPanSpeed;
                }
                else if (mouse.X >= this.ActualWidth - edgeDistance)
                {
                    x += autoPanSpeed;
                }

                if (mouse.Y <= edgeDistance)
                {
                    y -= autoPanSpeed;
                }
                else if (mouse.Y >= this.ActualHeight - edgeDistance)
                {
                    y += autoPanSpeed;
                }

                this.ViewportLocation = new Point(x, y);

                this.Point = Mouse.GetPosition(this.ItemsHost);
            }
        }
    }

    private async Task CopyToAsync(IList source, IList destination)
    {
        await Task.Run(() =>
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        });
    }

    public DependencyObject GetContainerForLineOverride(FluxoLineContainer container)
    {
        return new FluxoLine(this, container);
    }

    public bool IsLineItsOwnContainerOverride(FluxoLineContainer container, object item)
    {
        return item is FluxoLine;
    }

    private void OnNodeDragStarted(object sender, DragStartedEventArgs e)
    {
        if (e.OriginalSource is FluxoNode node)
        {
            this.RaiseEvent<FluxoNodeDragStartedEventArgs>(nameof(DragStared), new FluxoNodeDragStartedEventArgs(node));
        }
    }

    private void OnNodeDragCompleted(object sender, DragCompletedEventArgs e)
    {
        if (e.OriginalSource is FluxoNode node)
        {
            this.RaiseEvent<FluxoNodeDragCompletedEventArgs>(nameof(DragCompleted),
                new FluxoNodeDragCompletedEventArgs(node));
        }
    }

    private void OnNodeDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (e.OriginalSource is FluxoNode node)
        {
            var left = e.HorizontalChange;

            var top = e.VerticalChange;

            //TODO: MoveRange

            node.Location = new Point(left, top);

            this.RaiseEvent<FluxoNodeDragDeltaEventArgs>(nameof(DragDelta), new FluxoNodeDragDeltaEventArgs(node));
        }
    }

    private void OnRenderDot(DrawingContext dc)
    {
        var size = 22d;

        var column = this.ActualWidth / size;

        var row = this.ActualHeight / size;

        for (var i = 1; i < row; i++)
        {
            for (var j = 1; j < column; j++)
            {
                dc.DrawEllipse(Brushes.Gray, null, new Point(j * size, i * size), 1.5, 1.5);
            }
        }
    }

    private void OnRenderGrid(DrawingContext dc)
    {
        var size = 16d;

        var column = this.ActualWidth / size;
        var row = this.ActualHeight / size;

        var pen = new Pen(Brushes.Gray, 0.1);

        for (var i = 0; i < row; i++)
        {
            var y = i * size;

            dc.DrawLine(pen, new Point(0, y), new Point(this.ActualWidth, y));
        }

        for (var i = 0; i < column; i++)
        {
            var x = i * size;

            dc.DrawLine(pen, new Point(x, 0), new Point(x, this.ActualHeight));
        }
    }

    private static void OnCanAutoPanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FluxoDesigner designer && e.NewValue is bool canAutoPanning)
        {
            designer.OnAutoPanningChanged(canAutoPanning);
        }
    }

    private static void OnViewportLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FluxoDesigner designer && e.NewValue is Point translate)
        {
            designer._translateTransform.X = -translate.X * designer.Zoom;

            designer._translateTransform.Y = -translate.Y * designer.Zoom;

            designer.RaiseEvent(nameof(designer.ViewportUpdated), new RoutedEventArgs());
        }
    }
}