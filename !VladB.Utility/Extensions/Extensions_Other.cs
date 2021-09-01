using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VladB.Utility {
    public static partial class Extensions {
        
        public static TOut TryDo<TIn , TOut>(this TIn item , Func<TIn , TOut> func) where TIn : class where TOut : class {
            return item != null ? func.Invoke(item) : null;
        }

        public static void TryDo<TIn>(this TIn item, Action<TIn> func) where TIn : class{
            if(item != null) {
                func.Invoke(item);
            }
        }
    }
}
