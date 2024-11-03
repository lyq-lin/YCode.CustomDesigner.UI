namespace YCode.CustomDesigner.UI;

public class NodeDragStartedEventArgs(YCodeNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}