using System;
using System.Collections.Generic;

namespace VladB.Utility {
    public static partial class Extensions {
        static void DebugLog(string log) {
#if UNITY_5_3_OR_NEWER
            UnityEngine.Debug.Log(log);
#else
            Console.WriteLine(log);
#endif
        }

        //TODO Use StringBuilder
        public static void DebugAll<T>(this IList<T> list, bool isOneDebug = false, string separator = " ") {
            if(!isOneDebug) {
                for(int i = 0; i < list.Count; i++) {
                    DebugLog(list[i].ToString());
                }
            } else {
                string result = string.Empty;
                for(int i = 0; i < list.Count; i++) {
                    result += list[i] + ((i != list.Count - 1) ? separator : "");
                }
                DebugLog(result);
            }
        }

    }
}
