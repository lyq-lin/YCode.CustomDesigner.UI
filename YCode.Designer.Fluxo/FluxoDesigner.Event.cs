namespace YCode.Designer.Fluxo;

public partial class FluxoDesigner
{
    private readonly EventHandlerList _event = new();

    protected EventHandlerList Events => _event;

    #region Routed Event

    public event RoutedEventHandler ViewportUpdated
    {
        add => this.Events.AddHandler(nameof(ViewportUpdated), value);
        remove => this.Events.AddHandler(nameof(ViewportUpdated), value);
    }

    public event EventHandler<FluxoMountingEventArgs> Mounting
    {
        add => this.Events.AddHandler(nameof(Mounting), value);
        remove => this.Events.AddHandler(nameof(Mounting), value);
    }

    public event EventHandler<FluxoMountedEventArgs> Mounted
    {
        add => this.Events.AddHandler(nameof(Mounted), value);
        remove => this.Events.AddHandler(nameof(Mounted), value);
    }

    public event EventHandler<FluxoNodeDragStartedEventArgs> DragStared
    {
        add => this.Events.AddHandler(nameof(DragStared), value);
        remove => this.Events.AddHandler(nameof(DragStared), value);
    }

    public event EventHandler<FluxoNodeDragDeltaEventArgs> DragDelta
    {
        add => this.Events.AddHandler(nameof(DragDelta), value);
        remove => this.Events.AddHandler(nameof(DragDelta), value);
    }

    public event EventHandler<FluxoNodeDragCompletedEventArgs> DragCompleted
    {
        add => this.Events.AddHandler(nameof(DragCompleted), value);
        remove => this.Events.AddHandler(nameof(DragCompleted), value);
    }

    #endregion

    private void RaiseEvent(string eventName, RoutedEventArgs args)
    {
        if (_event[eventName] is RoutedEventHandler routed)
        {
            routed.Invoke(this, args as RoutedEventArgs);
        }
    }

    private void RaiseEvent<T>(string eventName, T args) where T : EventArgs
    {
        if (_event[eventName] is EventHandler<T> @event)
        {
            @event.Invoke(this, args);
        }

        if (_event[eventName] is EventHandler @event1)
        {
            @event1.Invoke(this, args);
        }
    }
}