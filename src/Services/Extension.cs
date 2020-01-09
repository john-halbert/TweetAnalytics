using System.Collections.Generic;
using System.Linq;

namespace TweetAnalytics.Extensions {
    public static class Extension {
        public static Dictionary<T, int> AddUsage<T>(this Dictionary<T, int> dest, Dictionary<T, int> source) {
            foreach (var item in source) {
                if (dest.ContainsKey (item.Key)) {
                    dest[item.Key] += item.Value;
                } else {
                    dest.Add (item.Key, item.Value);
                }
            }
            return dest;
        }
    }
}