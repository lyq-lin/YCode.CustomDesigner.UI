namespace YCode.Designer.Fluxo;

public class MountedEventArgs(YCodeSource? source) : EventArgs
{
    public YCodeSource? Source { get; } = source;
}