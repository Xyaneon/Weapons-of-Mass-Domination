using System.Collections.Generic;
using System.Linq;

namespace WMD.Game.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> collection) where T : class => collection.Where(element => element != null)!;
    }
}
