namespace YCode.Designer.Fluxo;

public class FluxoNodeDragDeltaEventArgs(FluxoNode node) : RoutedEventArgs
{
    public Point Location { get; } = node.Location;
    public UIElement Node { get; } = node;
}