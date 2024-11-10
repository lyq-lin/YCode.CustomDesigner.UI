namespace YCode.Designer.Fluxo;

public class FluxoConnector : Control
{
    static FluxoConnector()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FluxoConnector),
            new FrameworkPropertyMetadata(typeof(FluxoConnector))
        );
    }

    private FluxoNode? _node;
    private FluxoDataPanelItem? _item;
    private FluxoPendingLine? _line;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (this.FindParent<FluxoDataPanelItem>() is { } item)
        {
            _item = item;

            _node = item.Panel.Node;
        }

        if (_node is null && this.FindParent<FluxoNode>() is { } node)
        {
            _node = node;
        }

        if (_node != null)
        {
            _line ??= new FluxoPendingLine(_node.Designer, _node, _item?.ItemId ?? String.Empty);
        }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (_node != null)
        {
            e.Handled = true;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed)
        {
            return;
        }

        if (_node != null)
        {
            var layer = AdornerLayer.GetAdornerLayer(_node.Designer.ItemsHost);

            if (layer != null && _line != null)
            {
                layer.Add(_line);

                e.Handled = true;
            }
        }
    }
}