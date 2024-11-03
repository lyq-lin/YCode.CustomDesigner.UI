namespace YCode.CustomDesigner.UI;

public class NodeDragCompletedEventArgs(YCodeNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}