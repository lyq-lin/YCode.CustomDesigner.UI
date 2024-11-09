using System.Reflection;

namespace YCode.Designer.Fluxo;

internal class FluxoLineFactory
{
    private readonly Dictionary<LineType, IFluxoLineGeometry> _lines;

    public FluxoLineFactory(FluxoDesigner designer)
    {
        _lines = [];

        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (typeof(IFluxoLineGeometry).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            {
                if (Activator.CreateInstance(type, [designer]) is IFluxoLineGeometry model)
                {
                    _lines[model.Type] = model;
                }
            }
        }
    }

    public IFluxoLineGeometry? GetLine(LineType type) => _lines.GetValueOrDefault(type);
}