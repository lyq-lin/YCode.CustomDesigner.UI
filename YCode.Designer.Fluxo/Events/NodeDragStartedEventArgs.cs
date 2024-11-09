namespace YCode.Designer.Fluxo;

public class NodeDragStartedEventArgs(YCodeNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}