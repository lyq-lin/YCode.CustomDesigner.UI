using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using YCode.Designer.Fluxo;

namespace YCode.Designer.Demo;

public partial class Playground : UserControl
{
    public Playground()
    {
        InitializeComponent();
    }

    private void OnNodeDragDelta(object? sender, FluxoNodeDragDeltaEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragDelta)} Trigger...");
    }

    private void OnNodeDragStared(object? sender, FluxoNodeDragStartedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragStared)} Trigger...");
    }

    private void OnNodeDragCompleted(object? sender, FluxoNodeDragCompletedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragCompleted)} Trigger...");
    }

    private void OnMounting(object? sender, FluxoMountingEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnMounting)} Trigger...");
    }

    private void OnMounted(object? sender, FluxoMountedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnMounted)} Trigger...");
    }
}