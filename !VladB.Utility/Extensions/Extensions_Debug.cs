using System;
using System.Collections.Generic;


namespace VladB.Utility {
    public static partial class Extensions {
        public static void DebugAll<T>(this IList<T> list, bool isOneDebug = false, string separator = " ") {
            if(!isOneDebug) {
                for(int i = 0; i < list.Count; i++) {
#if UNITY_5_3_OR_NEWER
                    UnityEngine.Debug.Log(list[i].ToString());
#else
                    Console.WriteLine(list[i].ToString());
#endif

                }
            } else {
                string result = string.Empty;
                for(int i = 0; i < list.Count; i++) {
                    result += list[i] + ((i != list.Count - 1) ? separator : "");
                }
#if UNITY_5_3_OR_NEWER
                UnityEngine.Debug.Log(result);
#else
                Console.WriteLine(result);
#endif

            }
        }

    }
}
