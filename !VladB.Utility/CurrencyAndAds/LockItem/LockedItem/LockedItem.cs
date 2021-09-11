using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;
using System.Linq;

namespace VladB.Utility {
    public class LockedItem : MonoBehaviour {
        [Header("Locked Item Scriptable Object")]
        public Liso liso;

        [Header("UI")]
        public GameObject lockUI;

        public delegate void LockedItemUnlock();
        public event LockedItemUnlock OnLockedItemUnlock;

        public delegate void LockedItemUnlock_Static();
        public static event LockedItemUnlock_Static OnLockedItemUnlock_Static;

        List<ILockedItemUI> uis;

        void OnEnable() => OnEnableFunc();
        protected virtual void OnEnableFunc() {
            Init();
        }

        public virtual void Init() {
            if(liso == null) {
                SetActiveLockUI(false);
                return;
            }

            if(liso.isLockOnStart && !liso.available) {
                SetActiveLockUI(true);
            } else {
                SetActiveLockUI(false);
            }

            uis = GetComponents<ILockedItemUI>().ToList();
            uis.Act(item => item.Init(this));
            uis = uis.Where(item => item != null).ToList();
        }

        public virtual void Unlock() {
            liso.available = true;
            SetActiveLockUI(false);

            OnLockedItemUnlock?.Invoke();
            OnLockedItemUnlock_Static?.Invoke();
        }

        [ContextMenu("ClickOnLockUI")]
        public virtual void ClickOnLockUI() {
            uis.Act(item => item.ClickOnLockUI());
        }

        protected virtual void SetActiveLockUI(bool _isActive) {
            if(lockUI) {
                lockUI.SetActive(_isActive);
            } else {
                Debug.LogWarning("Item's lockUI not found!");
            }
        }
    }

}


public interface ILockedItemUI {
    public void Init(LockedItem lockedItem);
    public void ClickOnLockUI();
    public void UpdateUI();
}