namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    T Get<T>(string key);

    object Get(string key);

    /// <summary>
    /// Cache olarak eklenecek veriyi ekler.
    /// </summary>
    /// <param name="key">cache için eklenecek veri anahtar kelime</param>
    /// <param name="data">Cache için eklenecek data</param>
    /// <param name="duration">Cache'de kalma süresi</param>
    void Add(string key, object data, int duration);

    /// <summary>
    /// aşağodaki key Cache içerisine Eklenmiş mi 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool IsAdd(string key);

    /// <summary>
    /// Gelen key değerine karşılık gelen bütün cachle'leri siler
    /// </summary>
    /// <param name="key"></param>
    void Remove(string key);

    void RemoveByPattern(string pattern);

}