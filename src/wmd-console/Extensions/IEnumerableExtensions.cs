using System;
using System.Collections.Generic;

namespace WMD.Console.Extensions;

static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T element in collection)
        {
            action(element);
        }
    }
}
