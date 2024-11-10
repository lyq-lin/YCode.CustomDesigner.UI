namespace YCode.Designer.Fluxo;

public class FluxoDataPanelItem : ContentControl
{
    static FluxoDataPanelItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoDataPanelItem),
            new FrameworkPropertyMetadata(typeof(FluxoDataPanelItem))
        );
    }

    private readonly FluxoDataPanel _panel;

    public FluxoDataPanelItem(FluxoDataPanel panel)
    {
        _panel = panel;
    }

    internal FluxoDataPanel Panel => _panel ??
                                     throw new InvalidOperationException(
                                         $"FluxoDataPanel is missing or is not of type {nameof(FluxoDataPanel)}.");

    internal event EventHandler? Changed;

    #region Dependency Properties

    public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register(
        nameof(ItemId), typeof(string), typeof(FluxoDataPanelItem),
        new FrameworkPropertyMetadata(String.Empty));

    public string ItemId
    {
        get => (string)GetValue(ItemIdProperty);
        set => SetValue(ItemIdProperty, value);
    }

    #endregion
}