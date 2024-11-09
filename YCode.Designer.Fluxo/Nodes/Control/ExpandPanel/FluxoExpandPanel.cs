namespace YCode.Designer.Fluxo;

public class FluxoExpandPanel : HeaderedContentControl
{
    static FluxoExpandPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoExpandPanel),
            new FrameworkPropertyMetadata(typeof(FluxoExpandPanel))
        );
    }

    #region Dependency Properties

    public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register(
        nameof(HeaderBrush), typeof(Brush), typeof(FluxoExpandPanel), new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty IsExpandProperty = DependencyProperty.Register(
        nameof(IsExpand), typeof(bool), typeof(FluxoExpandPanel), new PropertyMetadata(true));

    public bool IsExpand
    {
        get => (bool)GetValue(IsExpandProperty);
        set => SetValue(IsExpandProperty, value);
    }

    public Brush HeaderBrush
    {
        get => (Brush)GetValue(HeaderBrushProperty);
        set => SetValue(HeaderBrushProperty, value);
    }

    #endregion
}