using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Ioc;

/// <summary>
/// Proje bağımlılığı olmayan, ortak Ioc'leri implement edebilmemizi sağlayan class
/// </summary>
public class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}