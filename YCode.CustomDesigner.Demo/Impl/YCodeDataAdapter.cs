using System.Windows;
using YCode.CustomDesigner.Demo.Helpers;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

public class YCodeDataAdapter : IYCodeAdapter
{
    public Task<YCodeSource> ImportAsync(object? value)
    {
        var viewModel = new YCodeSource();

        var count = 300;

        var xDistance = 220;

        var yDistance = 420;

        var size = (int)count / (int)Math.Sqrt(count);

        var nodes = RandomNodesGenerator.GenerateNodes<YCodeNodeViewModel>(
            count,
            count,
            (i, maxCount) => new Point(i % size * xDistance, i / size * yDistance),
            () =>
            {
                var context = new YCodeContext()
                {
                    IsExpand = false
                };

                for (var i = 0; i < 30; i++)
                {
                    context.Children.Add(new
                    {
                        Id = $"{i}",
                        Name = $"Column {i}",
                    });
                }

                return context;
            });

        viewModel.Nodes.AddRanage(nodes);

        return Task.FromResult(viewModel);
    }
}