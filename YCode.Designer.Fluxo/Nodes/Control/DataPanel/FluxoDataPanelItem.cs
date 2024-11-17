using System.Diagnostics;

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

        _panel.Node.Changed += OnChanged;
    }

    internal event EventHandler? Changed;

    internal FluxoDataPanel Panel => _panel ??
                                     throw new InvalidOperationException(
                                         $"FluxoDataPanel is missing or is not of type {nameof(FluxoDataPanel)}.");

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

    private void OnChanged(object? sender, EventArgs e)
    {
        var isChanged = false;

        var rect = new Rect(this.RenderSize);

        var point = this.TranslatePoint(new Point(0, 0), _panel.Node);

        var calcHeight = Math.Round(_panel.Node.Top.Y + point.Y + rect.Height / 2, 2);

        var headerPos = _panel.Node.Top.Y + 14d;

        var finalY = calcHeight < _panel.Node.Top.Y + 28d
            ? headerPos
            : calcHeight > _panel.Node.Bottom.Y
                ? headerPos
                : calcHeight;

        var left = new Point(_panel.Node.Left.X, finalY);

        var right = new Point(_panel.Node.Right.X, finalY);

        if (!_panel.Node.Points.TryGetValue(this.ItemId, out var value))
        {
            value = new FluxoPoint();

            _panel.Node.Points[this.ItemId] = value;
        }

        if (value.Left != left)
        {
            value.Left = left;

            isChanged = true;
        }

        if (value.Right != right)
        {
            value.Right = right;

            isChanged = true;
        }

        if (isChanged)
        {
            this.Changed?.Invoke(this, EventArgs.Empty);

            Debug.WriteLine($"Item:{value.Left},{value.Right}");
        }
    }
}