using System.Reflection;

namespace YCode.CustomDesigner.UI;

internal class YCodeLineFactory
{
    private readonly Dictionary<LineType, IYCodeLineGeometry> _lines;

    public YCodeLineFactory(YCodeDesigner designer)
    {
        _lines = [];

        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (typeof(IYCodeLineGeometry).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            {
                if (Activator.CreateInstance(type, [designer]) is IYCodeLineGeometry model)
                {
                    _lines[model.Type] = model;
                }
            }
        }
    }

    public IYCodeLineGeometry? GetLine(LineType type) => _lines.GetValueOrDefault(type);
}