namespace YCode.CustomDesigner.UI;

public class YCodeExpandPanel : HeaderedContentControl
{
    static YCodeExpandPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeExpandPanel),
            new FrameworkPropertyMetadata(typeof(YCodeExpandPanel))
        );
    }

    #region Dependency Properties

    public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register(
        nameof(HeaderBrush), typeof(Brush), typeof(YCodeExpandPanel), new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty IsExpandProperty = DependencyProperty.Register(
        nameof(IsExpand), typeof(bool), typeof(YCodeExpandPanel), new PropertyMetadata(true));

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