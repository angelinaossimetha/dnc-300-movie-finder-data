using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Caching;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;


//https://michaelscodingspot.com/cache-implementations-in-csharp-net/
public static class CacheModel
{
    //private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private static Dictionary<string, Movie> _movies = new Dictionary<string, Movie>();
       
public static Movie Get(string key)
    {
        foreach (KeyValuePair<string, Movie> entry in _movies)
        {
            Console.WriteLine(entry.Key + " : " + entry.Value);
        }
        Console.WriteLine($"get method {key}");
        return _movies[key];
    }

    public static void Add(string key, Movie movie)
    {
        Console.WriteLine("add method", key);
        _movies.Add(key, movie);
        Console.WriteLine("add method 1", _movies[key].Title);
        //_cache.Set(key, movie);
    }

    public static Boolean Contains(string key)
    {
       return _movies.ContainsKey(key);
    
    }
}


