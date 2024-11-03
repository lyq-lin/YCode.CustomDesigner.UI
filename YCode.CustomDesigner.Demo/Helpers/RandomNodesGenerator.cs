using System.Windows;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo.Helpers;

public static class RandomNodesGenerator
{
    private static readonly Random Rand = new Random();

    public static List<T> GenerateNodes<T>(
        int minNodesCount,
        int maxNodesCount,
        Func<int, int, Point> func = default!,
        Func<object> contextGenerator = default!) where T : YCodeNodeViewModel, new()
    {
        func ??= NodeLocationGenerator;

        contextGenerator ??= () => default!;

        var nodes = new List<T>();

        var count = Rand.Next(minNodesCount, maxNodesCount + 1);

        for (var i = 0; i < count; i++)
        {
            var nodeId = DateTime.Now.Ticks.ToString("X");

            var node = new T
            {
                Id = nodeId,
                Name = $"Node {i}",
                Description = $"This is a node {nodeId}",
                Location = func.Invoke(i, maxNodesCount),
                Context = contextGenerator.Invoke()
            };

            nodes.Add(node);
        }

        return nodes;
    }

    private static Point NodeLocationGenerator(int i, int count)
    {
        static double EaseOut(double percent, double increment, double start, double end, double total)
            => -end * (increment /= total) * (increment - 2) + start;

        var xDistanceBetweenNodes = Rand.Next(150, 350);
        var yDistanceBetweenNodes = Rand.Next(200, 350);
        var randSignX = Rand.Next(0, 100) > 50 ? 1 : -1;
        var randSignY = Rand.Next(0, 100) > 50 ? 1 : -1;
        var gridOffsetX = i * xDistanceBetweenNodes;
        var gridOffsetY = i * yDistanceBetweenNodes;

        var x = gridOffsetX * Math.Sin(xDistanceBetweenNodes * randSignX / (i + 1));
        var y = gridOffsetY * Math.Sin(yDistanceBetweenNodes * randSignY / (i + 1));
        var easeX = x * EaseOut(i / count, i, 1, 0.01, count);
        var easeY = y * EaseOut(i / count, i, 1, 0.01, count);

        x = easeX / 15 * 15;
        y = easeY / 15 * 15;

        return new Point(x, y);
    }
}