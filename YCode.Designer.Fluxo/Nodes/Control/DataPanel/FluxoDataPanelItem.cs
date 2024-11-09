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

    internal event EventHandler? Changed;

    #region Dependency Properties

    public static readonly DependencyProperty ColumnIdProperty = DependencyProperty.Register(
        nameof(ColumnId), typeof(string), typeof(FluxoDataPanelItem),
        new FrameworkPropertyMetadata(String.Empty));

    public string ColumnId
    {
        get => (string)GetValue(ColumnIdProperty);
        set => SetValue(ColumnIdProperty, value);
    }

    #endregion
}