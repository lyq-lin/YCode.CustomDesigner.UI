namespace YCode.CustomDesigner.UI;

public interface IYCodeAdapter
{
    Task<YCodeSource> ImportAsync(object? value);
}