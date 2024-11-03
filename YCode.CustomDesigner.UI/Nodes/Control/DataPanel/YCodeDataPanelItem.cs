namespace YCode.CustomDesigner.UI;

public class YCodeDataPanelItem : ContentControl
{

    static YCodeDataPanelItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeDataPanelItem),
            new FrameworkPropertyMetadata(typeof(YCodeDataPanelItem))
        );
    }

    private readonly YCodeDataPanel _panel;
    
    public YCodeDataPanelItem(YCodeDataPanel panel)
    {
        _panel = panel;
    }
    
    #region Dependency Properties

    public static readonly DependencyProperty ColumnIdProperty = DependencyProperty.Register(
        nameof(ColumnId), typeof(string), typeof(YCodeDataPanelItem),
        new FrameworkPropertyMetadata(String.Empty));

    public string ColumnId
    {
        get => (string)GetValue(ColumnIdProperty);
        set => SetValue(ColumnIdProperty, value);
    }

    #endregion
}