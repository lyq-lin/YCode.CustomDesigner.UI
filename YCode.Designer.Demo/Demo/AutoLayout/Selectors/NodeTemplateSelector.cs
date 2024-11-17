using System.Windows;
using System.Windows.Controls;
using YCode.Designer.Demo.Models;
using YCode.Designer.Fluxo;

namespace YCode.Designer.Demo;

public class NodeTemplateSelector : DataTemplateSelector
{
    public DataTemplate NormalTemplate { get; set; } = default!;

    public DataTemplate EmptyTemplate { get; set; } = default!;

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is FluxoNodeViewModel { Context: NodeContext context })
        {
            return context.Type switch
            {
                NodeType.Empty => this.EmptyTemplate,
                NodeType.Normal or
                    _ => this.NormalTemplate,
            };
        }

        return base.SelectTemplate(item, container);
    }
}