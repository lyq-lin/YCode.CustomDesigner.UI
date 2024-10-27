using System.Windows;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

internal class MainViewModel : YCodeNotifyPropertyChanged
{
    public YCodeSource Source { get; set; }

    public MainViewModel()
    {
        this.Source = new YCodeSource();

        this.Source.Nodes.Add(new YCodeNodeViewModel()
        {
            Id = DateTime.Now.Ticks.ToString("X"),
            Name = "Node A",
            Description = "This is a node A.",
            Location = new Point(100, 180)
        });

        this.Source.Nodes.Add(new YCodeNodeViewModel()
        {
            Id = DateTime.Now.Ticks.ToString("X"),
            Name = "Node B",
            Description = "This is a node B.",
            Location = new Point(200, 300)
        });
    }
}