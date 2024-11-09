namespace YCode.Designer.Fluxo;

public class NodeDragDeltaEventArgs(YCodeNode node) : RoutedEventArgs
{
    public Point Location { get; } = node.Location;
    public UIElement Node { get; } = node;
}