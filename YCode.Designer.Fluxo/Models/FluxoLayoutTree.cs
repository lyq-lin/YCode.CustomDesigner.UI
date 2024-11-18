namespace YCode.Designer.Fluxo;

internal class FluxoLayoutTree(FluxoNode node)
{
    public FluxoNode Node { get; set; } = node;

    public FluxoLayoutTree? Parent { get; set; }

    public int Depth { get; set; }

    public List<FluxoLayoutTree> Nexts { get; } = [];

    public void AddNext(FluxoLayoutTree tree)
    {
        tree.Parent = this;

        this.Nexts.Add(tree);
    }
}