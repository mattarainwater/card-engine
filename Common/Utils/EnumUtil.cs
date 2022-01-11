using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EnumUtil
{
    private static Random Random = new Random();

    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static T RandomEnum<T>() where T : struct, IConvertible
    {
        Type type = typeof(T);
        Array values = Enum.GetValues(type);
        lock (Random)
        {
            object value = values.GetValue(Random.Next(values.Length));
            return (T)Convert.ChangeType(value, type);
        }
    }
}
