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

    protected internal Panel ItemsHost { get; set; } = default!;

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

        this.ItemsHost = this.GetTemplateChild("PART_ItemsHost") as Panel ??
                         throw new InvalidOperationException("PART_ItemsHost is missing or is not of type Panel.");
    }
}