namespace YCode.Designer.Fluxo;

public class NodeDragCompletedEventArgs(YCodeNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}