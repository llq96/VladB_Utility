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
        public void SetLockItem(int _index, bool _isNeedInitialize = true) {
            if((_index >= 0) && (_index < list.Count)) {
                LockedItemSetter.Set(lockedItem, list[_index], _isNeedInitialize);
            }
        }

        public void SetLockItem(int _index, LockedItem _customLockItem, bool _isNeedInitialize = true) {
            if((_index >= 0) && (_index < list.Count)) {
                LockedItemSetter.Set(_customLockItem, list[_index], _isNeedInitialize);
            }
        }
        #endregion

        #region SetLockedItems
        public void SetLockItems(LockedItem[] _customLockItems, bool _isNeedInitialize = true) {
            for(int i = 0; i < _customLockItems.Length; i++) {
                SetLockItem(i, _customLockItems[i], _isNeedInitialize);
            }
        }

        public void SetLockItems(List<LockedItem> _customLockItems, bool _isNeedInitialize = true) {
            SetLockItems(_customLockItems.ToArray(), _isNeedInitialize);
        }
        #endregion
    }
}