using System.Collections.Generic;
using UnityEngine;

namespace Common {
    public static class CachedResources {
        private static readonly Dictionary<string, object> Cache =
            new Dictionary<string,object>();

        public static T Load<T>(string path) where T : Object {
            if (Cache.TryGetValue(path, out var cached)) {
                if (cached is T value) {
                    return value;
                }

                return null;
            }

            var loaded = Resources.Load<T>(path);
            Cache[path] = loaded;

            return loaded;
        }

        public static void Save(string path, object value) {
            Cache[path] = value;
        }
    }
}