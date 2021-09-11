using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class LockedItemSetter : MonoBehaviour {
        public static bool Set(LockedItem lockitem, Liso liso, bool isNeedInitialize = true) {
            //Debug.Log("LI");
            if(lockitem == null) {
                return false;
            }
            if(liso == null) {
                Debug.LogError("Error");
                return false;
            }

            lockitem.liso = liso;

            if(isNeedInitialize) {
                lockitem.Init();
            }

            return true;
        }
    }
}