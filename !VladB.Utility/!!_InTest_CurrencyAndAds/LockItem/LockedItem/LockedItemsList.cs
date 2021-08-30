using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class LockedItemsList : MonoBehaviour {
        [Header("List of Locked Items Scriptable Objects")]
        public List<Liso> list;

        [Header("Locked Item")]
        public LockedItem lockedItem;

        #region SetLockedItem
        public void SetLockItem(int index, bool isNeedInitialize = true) {
            if((index >= 0) && (index < list.Count)) {
                LockedItemSetter.Set(lockedItem, list[index], isNeedInitialize);
            }
        }

        public void SetLockItem(int index, LockedItem customLockItem, bool isNeedInitialize = true) {
            if((index >= 0) && (index < list.Count)) {
                LockedItemSetter.Set(customLockItem, list[index], isNeedInitialize);
            }
        }
        #endregion

        #region SetLockedItems
        public void SetLockItems(LockedItem[] customLockItems, bool isNeedInitialize = true) {
            for(int i = 0; i < customLockItems.Length; i++) {
                SetLockItem(i, customLockItems[i], isNeedInitialize);
            }
        }

        public void SetLockItems(List<LockedItem> customLockItems, bool isNeedInitialize = true) {
            SetLockItems(customLockItems.ToArray(), isNeedInitialize);
        }
        #endregion
    }
}