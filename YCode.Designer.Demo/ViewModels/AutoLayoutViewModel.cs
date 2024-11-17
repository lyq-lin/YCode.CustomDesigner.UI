using System.Windows;
using YCode.Designer.Demo.Models;
using YCode.Designer.Fluxo;

namespace YCode.Designer.Demo;

public class AutoLayoutViewModel : FluxoNotifyPropertyChanged
{
    public AutoLayoutViewModel()
    {
        this.Source = new FluxoSource();

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "Root",
            Name = "Root",
            Location = new Point(122, 240),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "If1",
            Name = "Branch",
            Location = new(399, 240),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "Process1",
            Name = "Process",
            Location = new(799, 177),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "Process2",
            Name = "Process",
            Location = new(799, 296.5),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "IfEnd1",
            Location = new(1729, 303),
            Context = new NodeContext()
            {
                Type = NodeType.Empty
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "If2",
            Name = "Branch",
            Location = new(1029, 177),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "Process3",
            Name = "Process",
            Location = new(1306, 228.5),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "Process4",
            Name = "Process",
            Location = new(1306, 142),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "IfEnd2",
            Location = new(1618, 207),
            Context = new NodeContext()
            {
                Type = NodeType.Empty
            }
        });

        this.Source.Nodes.Add(new FluxoNodeViewModel()
        {
            Id = "End",
            Name = "End",
            Location = new(1917, 290),
            Context = new NodeContext()
            {
                Type = NodeType.Normal
            }
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "Root",
            NextId = "If1"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "If1",
            NextId = "Process1"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "Process1",
            NextId = "If2"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "If2",
            NextId = "Process3"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "If2",
            NextId = "Process4"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "Process3",
            NextId = "IfEnd2"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "Process4",
            NextId = "IfEnd2"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "IfEnd2",
            NextId = "IfEnd1"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "If1",
            NextId = "Process2"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "Process2",
            NextId = "IfEnd1"
        });

        this.Source.Lines.Add(new FluxoLineViewModel()
        {
            PrevId = "IfEnd1",
            NextId = "End"
        });
    }

    public FluxoSource Source { get; set; }
}