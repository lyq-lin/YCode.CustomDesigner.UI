using System.Windows;
using YCode.CustomDesigner.Demo.Helpers;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

public class YCodeDataAdapter : IYCodeAdapter
{
    public Task<YCodeSource> ImportAsync(object? value)
    {
        var viewModel = new YCodeSource();

        var count = 800;

        var distance = 220;

        var size = (int)count / (int)Math.Sqrt(count);

        var nodes = RandomNodesGenerator.GenerateNodes<YCodeNodeViewModel>(count, count,
            (i, maxCount) => new Point(i % size * distance, i / size * distance));

        viewModel.Nodes.AddRanage(nodes);

        return Task.FromResult(viewModel);
    }
}