using Core.Utilities.Ioc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private IMemoryCache _cache;
    public MemoryCacheManager()
    {
        _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        if (_cache is null)
        {
            new ArgumentNullException("Memory Cache Bulunamadı !");
        }
    }
    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public object Get(string key)
    {
        return _cache.Get(key);
    }

    public void Add(string key, object data, int duration)
    {
        _cache.Set(key, data, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _cache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        throw new NotImplementedException();
    }
}