using System;
using System.Collections.Generic;


namespace VladB.Utility {
    public static partial class Extensions {
        public static void Act<T>(this IList<T> list, Action<T, int> action) {
            for(int i = 0; i < list.Count; i++) {
                action.Invoke(list[i], i);
            }
        }
    }
}
