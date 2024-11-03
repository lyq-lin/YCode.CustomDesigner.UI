namespace YCode.CustomDesigner.UI;

public partial class YCodeDesigner
{
    private readonly EventHandlerList _event = new();

    protected EventHandlerList Events => _event;

    #region Routed Event

    public event RoutedEventHandler ViewportUpdated
    {
        add => this.Events.AddHandler(nameof(ViewportUpdated), value);
        remove => this.Events.AddHandler(nameof(ViewportUpdated), value);
    }

    public event EventHandler<MountingEventArgs> Mounting
    {
        add => this.Events.AddHandler(nameof(Mounting), value);
        remove => this.Events.AddHandler(nameof(Mounting), value);
    }

    public event EventHandler<MountedEventArgs> Mounted
    {
        add => this.Events.AddHandler(nameof(Mounted), value);
        remove => this.Events.AddHandler(nameof(Mounted), value);
    }

    public event EventHandler<NodeDragStartedEventArgs> DragStared
    {
        add => this.Events.AddHandler(nameof(DragStared), value);
        remove => this.Events.AddHandler(nameof(DragStared), value);
    }

    public event EventHandler<NodeDragDeltaEventArgs> DragDelta
    {
        add => this.Events.AddHandler(nameof(DragDelta), value);
        remove => this.Events.AddHandler(nameof(DragDelta), value);
    }

    public event EventHandler<NodeDragCompletedEventArgs> DragCompleted
    {
        add => this.Events.AddHandler(nameof(DragCompleted), value);
        remove => this.Events.AddHandler(nameof(DragCompleted), value);
    }

    #endregion

    private void RaiseEvent(string eventName, EventArgs args)
    {
        if (_event[eventName] is RoutedEventHandler routed)
        {
            routed.Invoke(this, args as RoutedEventArgs);
        }

        if (_event[eventName] is EventHandler @event)
        {
            @event.Invoke(this, args);
        }
    }

    private void RaiseEvent<T>(string eventName, T args)
    {
        if (_event[eventName] is EventHandler<T> @event)
        {
            @event.Invoke(this, args);
        }
    }
}