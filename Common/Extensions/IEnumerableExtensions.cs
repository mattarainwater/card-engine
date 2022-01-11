using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IEnumerableExtensions
{
    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).SingleOrDefault();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source != null ? source.OrderBy(x => Guid.NewGuid()) : Enumerable.Empty<T>();
    }

    public static T Draw<T>(this List<T> source)
    {
        if (source.Count() == 0)
            return default(T);

        int index = 0;
        var result = source[index];
        source.RemoveAt(index);
        return result;
    }

    public static List<T> Draw<T>(this List<T> list, int count)
    {
        int resultCount = Math.Min(count, list.Count);
        List<T> result = new List<T>(resultCount);
        for (int i = 0; i < resultCount; ++i)
        {
            T item = list.Draw();
            result.Add(item);
        }
        return result;
    }
}