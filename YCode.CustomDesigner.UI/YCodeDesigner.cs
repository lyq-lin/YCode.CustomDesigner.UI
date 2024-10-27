using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YCode.CustomDesigner.UI;

public class YCodeDesigner : MultiSelector
{
    static YCodeDesigner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeDesigner),
            new FrameworkPropertyMetadata(typeof(YCodeDesigner))
        );

        FocusableProperty.OverrideMetadata(typeof(YCodeDesigner), new FrameworkPropertyMetadata(true));
    }

    private readonly TranslateTransform _translateTransform = new();
    private readonly ScaleTransform _scaleTransform = new();

    public YCodeDesigner()
    {
        var transform = new TransformGroup();
        transform.Children.Add(_scaleTransform);
        transform.Children.Add(_translateTransform);

        this.SetValue(ViewportTransformPropertyKey, transform);
    }

    protected internal Panel ItemsHost { get; private set; } = default!;

    #region Dependency Properties

    protected internal static readonly DependencyPropertyKey ViewportTransformPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(ViewportTransform),
            typeof(Transform),
            typeof(YCodeDesigner),
            new FrameworkPropertyMetadata(new TransformGroup())
        );

    public static readonly DependencyProperty ViewportTransformProperty =
        ViewportTransformPropertyKey.DependencyProperty;

    public static readonly DependencyProperty ItemsExtentProperty = DependencyProperty.Register(
        nameof(ItemsExtent), typeof(Rect), typeof(YCodeDesigner), new PropertyMetadata(default(Rect)));

    public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
        nameof(Zoom), typeof(double), typeof(YCodeDesigner), new PropertyMetadata(1d));

    public static readonly DependencyProperty MaxZoomProperty = DependencyProperty.Register(
        nameof(MaxZoom), typeof(double), typeof(YCodeDesigner), new PropertyMetadata(2d));

    public static readonly DependencyProperty MinZoomProperty = DependencyProperty.Register(
        nameof(MinZoom), typeof(double), typeof(YCodeDesigner), new PropertyMetadata(0.5d));

    public static readonly DependencyProperty ViewportLocationProperty = DependencyProperty.Register(
        nameof(ViewportLocation), typeof(Point), typeof(YCodeDesigner), new PropertyMetadata(default(Point)));

    public static readonly DependencyProperty ViewportSizeProperty = DependencyProperty.Register(
        nameof(ViewportSize), typeof(Size), typeof(YCodeDesigner), new PropertyMetadata(default(Size)));

    public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
        nameof(SelectedItems), typeof(IList), typeof(YCodeDesigner), new PropertyMetadata(default(IList)));

    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        nameof(Source), typeof(YCodeSource), typeof(YCodeDesigner));

    public YCodeSource Source
    {
        get => (YCodeSource)GetValue(SourceProperty);
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

    #region Routed Event

    public static readonly RoutedEvent ViewportUpdatedEvent = EventManager.RegisterRoutedEvent(
        nameof(ViewportUpdated),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(YCodeDesigner)
    );

    public event RoutedEventHandler ViewportUpdated
    {
        add => AddHandler(ViewportUpdatedEvent, value);
        remove => RemoveHandler(ViewportUpdatedEvent, value);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.ItemsHost = this.GetTemplateChild("PART_ItemsHost") as Panel ??
                         throw new InvalidOperationException("PART_ItemsHost is missing or is not of type Panel.");
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.Property == SourceProperty)
        {
            if (e.NewValue is YCodeSource source)
            {
                this.ItemsSource = source.Nodes;

                //TODO: Lines Adapter

                return;
            }
        }

        base.OnPropertyChanged(e);
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new YCodeNode(this);
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is YCodeNode;
    }
}