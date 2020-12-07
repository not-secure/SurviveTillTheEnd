using System.Collections.Generic;
using UnityEngine;

namespace Common {
    public static class ThrottleDebug {
        private static readonly Dictionary<string, float> LastInvoked = new Dictionary<string, float>();

        public static void Log(string key, object message) {
            if (LastInvoked.ContainsKey(key)) {
                if (LastInvoked[key] > Time.time - 1f) return;
            }
            LastInvoked[key] = Time.time;
            
            Debug.Log(message);
        }
    }
}