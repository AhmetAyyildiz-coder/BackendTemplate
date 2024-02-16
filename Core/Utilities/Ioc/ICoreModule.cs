using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Ioc;

public interface ICoreModule
{
    public void Load(IServiceCollection collection);
}