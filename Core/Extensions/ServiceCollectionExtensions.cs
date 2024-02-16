using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

/// <summary>
/// Api projeleri için merkezi olarak dependency'leri takip edebilmemizi sağlar.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
    {
        foreach (var module in modules)
        {
            module.Load(services);
        }

        return ServiceTool.Create(services);
    }
}