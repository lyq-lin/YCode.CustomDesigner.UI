using System.Diagnostics;
using System.Windows.Controls;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

public partial class Performance : UserControl
{
    public Performance()
    {
        InitializeComponent();
    }
    
    private void OnMounting(object? sender, MountingEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnMounting)} Trigger...");
    }

    private void OnMounted(object? sender, MountedEventArgs e)
    {
        Debug.WriteLine($"{nameof(OnMounted)} Trigger...");
    }
}