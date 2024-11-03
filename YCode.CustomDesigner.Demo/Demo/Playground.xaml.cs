using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

public partial class Playground : UserControl
{
    public Playground()
    {
        InitializeComponent();
    }

    private void OnNodeDragDelta(object? sender, NodeDragDeltaEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragDelta)} Trigger...");
    }

    private void OnNodeDragStared(object? sender, NodeDragStartedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragStared)} Trigger...");
    }

    private void OnNodeDragCompleted(object? sender, NodeDragCompletedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnNodeDragCompleted)} Trigger...");
    }
}