namespace YCode.CustomDesigner.UI;

public class MountedEventArgs(YCodeSource? source) : EventArgs
{
    public YCodeSource? Source { get; } = source;
}