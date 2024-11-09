namespace YCode.Designer.Fluxo;

public interface IYCodeAdapter
{
    Task<YCodeSource> ImportAsync(object? value);
}