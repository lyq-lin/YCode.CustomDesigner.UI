namespace YCode.Designer.Fluxo;

public interface IFluxoAdapter
{
    Task<FluxoSource> ImportAsync(object? value);
}