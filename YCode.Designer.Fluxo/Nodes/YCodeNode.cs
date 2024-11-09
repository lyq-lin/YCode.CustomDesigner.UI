using System.Runtime.CompilerServices;

namespace YCode.Designer.Fluxo;

public class YCodeNode : ContentControl, IDesignerItem
{
    static YCodeNode()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeNode),
            new FrameworkPropertyMetadata(typeof(YCodeNode))
        );
    }

    private readonly YCodeDesigner _designer;
    private Point? _point;

    public YCodeNode(YCodeDesigner designer)
    {
        _designer = designer;

        this.Lines = [];

        this.LayoutUpdated += OnLayoutUpdateChanged;
    }

    internal event EventHandler? Changed;

    internal YCodeDesigner Designer => _designer;
    internal Point Left { get; private set; }
    internal Point Right { get; private set; }
    internal Point Top { get; private set; }
    internal Point Bottom { get; private set; }

    internal List<YCodeLine> Lines { get; private set; }

    #region Dependency Properties

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
        nameof(Location), typeof(Point), typeof(YCodeNode),
        new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsArrange,
            OnLocationChanged));

    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId), typeof(string), typeof(YCodeNode), new PropertyMetadata(String.Empty));

    public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(nameof(IsSelectable),
        typeof(bool), typeof(YCodeNode), new FrameworkPropertyMetadata(true));

    public static readonly DependencyProperty IsSelectedProperty =
        Selector.IsSelectedProperty.AddOwner(typeof(YCodeNode),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsSelectedChanged));

    public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(
        nameof(SelectedBrush), typeof(Brush), typeof(YCodeNode), new PropertyMetadata(Brushes.Orange));

    public Brush SelectedBrush
    {
        get => (Brush)GetValue(SelectedBrushProperty);
        set => SetValue(SelectedBrushProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public bool IsSelectable
    {
        get => (bool)GetValue(IsSelectableProperty);
        set => SetValue(IsSelectableProperty, value);
    }

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

    #region Routed Event

    public static readonly RoutedEvent DragStartedEvent = EventManager.RegisterRoutedEvent(nameof(DragStarted),
        RoutingStrategy.Bubble, typeof(DragStartedEventHandler), typeof(YCodeNode));

    public static readonly RoutedEvent DragCompletedEvent = EventManager.RegisterRoutedEvent(nameof(DragCompleted),
        RoutingStrategy.Bubble, typeof(DragCompletedEventHandler), typeof(YCodeNode));

    public static readonly RoutedEvent DragDeltaEvent = EventManager.RegisterRoutedEvent(nameof(DragDelta),
        RoutingStrategy.Bubble, typeof(DragDeltaEventHandler), typeof(YCodeNode));

    public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof(YCodeNode));
    public static readonly RoutedEvent UnselectedEvent = Selector.UnselectedEvent.AddOwner(typeof(YCodeNode));

    public static readonly RoutedEvent LocationChangedEvent = EventManager.RegisterRoutedEvent(nameof(LocationChanged),
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(YCodeNode));

    public event RoutedEventHandler LocationChanged
    {
        add => AddHandler(LocationChangedEvent, value);
        remove => RemoveHandler(LocationChangedEvent, value);
    }

    public event DragStartedEventHandler DragStarted
    {
        add => AddHandler(DragStartedEvent, value);
        remove => RemoveHandler(DragStartedEvent, value);
    }

    public event DragDeltaEventHandler DragDelta
    {
        add => AddHandler(DragDeltaEvent, value);
        remove => RemoveHandler(DragDeltaEvent, value);
    }

    public event DragCompletedEventHandler DragCompleted
    {
        add => AddHandler(DragCompletedEvent, value);
        remove => RemoveHandler(DragCompletedEvent, value);
    }

    public event RoutedEventHandler Selected
    {
        add => AddHandler(SelectedEvent, value);
        remove => RemoveHandler(SelectedEvent, value);
    }

    public event RoutedEventHandler Unselected
    {
        add => AddHandler(UnselectedEvent, value);
        remove => RemoveHandler(UnselectedEvent, value);
    }

    #endregion

    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
    {
        this.Focus();

        this.Designer.UnselectAll();

        this.IsSelected = true;
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (!this.IsMouseCaptured)
        {
            this.CaptureMouse();
        }

        _point = e.GetPosition(this);

        this.RaiseEvent(new DragStartedEventArgs(_point.Value.X, _point.Value.Y)
        {
            RoutedEvent = DragStartedEvent
        });
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed)
        {
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            return;
        }

        var mouse = e.GetPosition(_designer.ItemsHost);

        if (_point.HasValue)
        {
            this.InvalidateVisual();

            var delta = mouse - _point.Value;

            this.RaiseEvent(new System.Windows.Controls.Primitives.DragDeltaEventArgs(delta.X, delta.Y)
            {
                RoutedEvent = DragDeltaEvent
            });
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        if (this.IsMouseCaptured)
        {
            if (_point.HasValue)
            {
                this.RaiseEvent(new DragCompletedEventArgs(this.Location.X, this.Location.Y, false)
                {
                    RoutedEvent = DragCompletedEvent
                });

                _point = null;
            }

            this.ReleaseAllTouchCaptures();
        }
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

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is YCodeNode node && e.NewValue is bool isSelected)
        {
            var canSelected = node.IsSelectable && isSelected;

            node.IsSelected = canSelected;

            node.RaiseEvent(new RoutedEventArgs(canSelected ? SelectedEvent : UnselectedEvent, node));
        }
    }

    private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is YCodeNode node)
        {
            node.Designer.ItemsHost.InvalidateArrange();

            node.RaiseEvent(new RoutedEventArgs(LocationChangedEvent, node));
        }
    }
}