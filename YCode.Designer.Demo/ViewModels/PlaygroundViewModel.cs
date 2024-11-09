using System.Windows;
using YCode.Designer.Fluxo;

namespace YCode.Designer.Demo;

public class PlaygroundViewModel : YCodeNotifyPropertyChanged
{
    public YCodeSource Source { get; set; }

    public PlaygroundViewModel()
    {
        this.Source = new YCodeSource();

        var sourceId = DateTime.Now.Ticks.ToString("X");
        var targetId = DateTime.Now.Ticks.ToString("X");

        this.Source.Nodes.Add(new YCodeNodeViewModel()
        {
            Id = sourceId,
            Name = "Node A",
            Description = "This is a node A.",
            Location = new Point(100, 180)
        });

        this.Source.Lines.Add(new YCodeLineViewModel()
        {
            PrevId = sourceId,
            NextId = targetId
        });

        this.Source.Nodes.Add(new YCodeNodeViewModel()
        {
            Id = targetId,
            Name = "Node B",
            Description = "This is a node B.",
            Location = new Point(400, 300)
        });
    }
}