using System.Collections.Generic;
using System.Linq;

namespace Fithub.UI.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self?.Select((item, index) => (item, index)) ?? Enumerable.Empty<(T, int)>();
        }

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                self.Add(item);
            }
        }
    }
}
